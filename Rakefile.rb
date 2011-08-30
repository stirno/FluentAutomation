#--------------------------------------
# Dependencies
#--------------------------------------
require 'albacore'
require 'net/ssh'
require 'net/scp'
#--------------------------------------
# Debug
#--------------------------------------
#ENV.each {|key, value| puts "#{key} = #{value}" }
#--------------------------------------
# My environment vars
#--------------------------------------
@env_projectname = ENV['env_projectname']
@env_buildconfigname = ENV['env_buildconfigname']
@env_buildversion = ENV['env_buildversion']
@env_buildnumber = @env_buildversion.match(/\d+\.\d+\.\d+\.(\d+)/)[1]
@env_projectfullname = ENV['env_projectfullname']
@env_buildfolderpath = ENV['env_buildfolderpath'].gsub(%r{\\}) { "/" }
@env_scpserver = ENV['env_scpserver']
@env_scpuser = ENV['env_scpuser']
@env_scppassword = ENV['env_scppassword']
@env_scppath = ENV['env_scppath']
#--------------------------------------
# Albacore flow controlling tasks
#--------------------------------------
desc "Creates ZIP and NuGet packages."
task :default => [:copyBinaries, :createZipPackage, :uploadPackage, :cleanUp]
#, :createNuGetPackage]
#--------------------------------------
# Albacore tasks
#--------------------------------------
desc "Copy binaries to output."
task :copyBinaries do
	puts "#{@env_buildfolderpath}SourceCode/bin/#{@env_buildconfigname}/*.*"
	puts "#{@env_buildfolderpath}Binaries/"
	
	if File.directory?("#{@env_buildfolderpath}Binaries/")
		FileUtils.rm_rf(FileList["#{@env_buildfolderpath}Binaries/*.*"])
	else
		FileUtils.mkdir("#{@env_buildfolderpath}Binaries/")
	end
	
	FileUtils.cp_r(FileList["#{@env_buildfolderpath}SourceCode/bin/#{@env_buildconfigname}/*.*"], "#{@env_buildfolderpath}Binaries/")
end

desc "Creates ZIPs package of binaries folder."
zip :createZipPackage do |zip|
	puts "#{@env_buildfolderpath}Binaries/"
	puts "#{@env_projectfullname}#{@env_buildnumber}.zip"
	zip.directories_to_zip "#{@env_buildfolderpath}Binaries/"
	zip.output_file = "#{@env_projectfullname}#{@env_buildnumber}.zip"
	zip.output_path = @env_buildfolderpath
end

desc "Upload Package to build storage via SCP"
task :uploadPackage do
	Net::SSH.start(@env_scpserver, @env_scpuser, :password => @env_scppassword) do |ssh|
		ssh.scp.upload!("#{@env_projectfullname}#{@env_buildnumber}.zip", "#{@env_scppath}") do |ch, name, sent, total|
			print "\r#{name}: #{(sent.to_f * 100 / total.to_f).to_i}%"
		end
	end
end

desc "Clean up packages on build agent"
task :cleanUp do
	FileUtils.rm_rf("#{@env_buildfolderpath}Binaries/")
	FileUtils.rm_rf(FileList["#{@env_buildfolderpath}*.metaproj*"])
	FileUtils.rm_rf(FileList["#{@env_buildfolderpath}*.zip"])
end

#desc "Creates NuGet package"
#exec :createNuGetPackage do |cmd|
#  cmd.command = "NuGet.exe"
#  cmd.parameters = "pack #{@env_projectname}.nuspec -version #{@env_buildversion} -nodefaultexcludes -outputdirectory #{@env_buildfolderpath} -basepath #{@env_buildfolderpath}Binaries"
#end