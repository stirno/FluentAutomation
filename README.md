## Jekyll Base ##
This is a base that will ge you started with jekyll create by Daniel McGraw (@danielmcgraw).

### Usage ###
Check out my [post series](http://danielmcgraw.com/2011/04/14/The-Ultimate-Guide-To-Getting-Started-With-Jekyll-Part-1/) on how to use Jekyll Base to create your own Jekyll powered blog.

### Structure ###
<pre>
.  
|-- .gitignore  
|-- README  
|-- _config.yml  
|-- _layouts  
|   |-- layout.html  
|   `-- post.html  
|-- _posts  
|   `-- 1985-10-26-Test-Post.md  
`-- index.html  
</pre>

Lets take a look at what each of these do.

### .gitignore ###
This file is not manditory for a proper Jekyll install, but is useful if you are like me and use a mac (ignore the DS_Store) or emacs (ignore the autosave files). If you have any other files or folders that need ignoring toss them in here.

### README ###
This file is not manditory for a proper Jekyll install, but is recomended by GitHub for all repositories. Toss a simple description of your site and its make up in here if you would like.

### _config.yml ###
This is where you will be putting your Jekyll configuration options. If this file is omitted Jekyll will use its defualts to build your site. You can find the configuration options and default configuration [here](https://github.com/mojombo/jekyll/wiki/configuration).

### _layouts ###
This folder is where you will be putting all your layout templates. I have added layout.html and post.html so you can get an idea of how they are strutured and used. 

#### layout.html ####
This is the base template for our site. There are no naming conventions, but if you choose to change this file's name make sure you update all the layout references in your file's YAML Front Matter blocks.

#### post.html ####
This is the bast template for each of our posts. Again there are no naming conventions, but make sure you update the required files YAML Front Matter blocks if you do change its name. To learn more about the use of YAML Front Matter check out [this page](https://github.com/mojombo/jekyll/wiki/yaml-front-matter).

### _posts ###
This is your posts folder. You will be putting your blog posts in here. Notice the naming convention that is used. You will want to name your files with the the publish date preceeding the posts title all seperated by dashes (Year-Month-Day-Title-Of-The-Post.md). The post date that you see is pulled straight from this filename so make sure you lable your files right.
 
#### 1985-10-26-Test-Post.md ####
This is a simple blog post. Notice that we are using markdown. To learn more about markdown check out the [markdown syntax documentation](http://daringfireball.net/projects/markdown/syntax). Also notice that there is YAML Front Matter in this file specifying the layout it will use and the title of the post. Layout is one of a couple predefined global variables. You can also specify custom variables in the YAML Front Matter. To learn more about the use of YAML Front Matter check out [this page](https://github.com/mojombo/jekyll/wiki/yaml-front-matter).

### index.html ###
This is used to render your sites index. It is essntially a post loop wrapped in your base layout.
