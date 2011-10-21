using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Providers;
using FluentAutomation.API;
using System.Reflection;
using System.Linq.Expressions;
using FluentAutomation.API.Enumerations;

namespace FluentAutomation.RemoteCommands
{
    public class RemoteCommandManager
    {
        public void Execute(AutomationProvider provider, IEnumerable<RemoteCommand> commands)
        {
            CommandManager manager = new CommandManager(provider);
            Assembly asm = typeof(RemoteCommandManager).Assembly;

            try
            {
                manager.Record();

                var browserList = new List<BrowserType>();

                foreach (var command in commands)
                {
                    // attempt to locate mapper
                    // TODO: Get rid of the 'magic string' Commands part, make this work with loaded assemblies
                    var type = asm.GetType(string.Format("{0}.{1}.{2}", typeof(RemoteCommandManager).Namespace, "Commands", command.Name));
                    if (type == null)
                    {
                        throw new ArgumentException(string.Format("Unable to locate available command: {0}", command.Name));
                    }

                    CommandArgumentsTypeAttribute commandArgs = (CommandArgumentsTypeAttribute)type.GetCustomAttributes(typeof(CommandArgumentsTypeAttribute), false).FirstOrDefault();
                    if (commandArgs == null)
                    {
                        provider.Cleanup();
                        throw new ArgumentException(string.Format("Unable to locate command arguments handler for command: {0}", command.Name));
                    }

                    ICommand cmd = (ICommand)Activator.CreateInstance(type);

                    ICommandArguments args = null;
                    try
                    {
                        args = DeserializeArguments(commandArgs.ArgsType, command.Arguments);
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException(string.Format("An error occurred while processing the arguments provided for command: {0}", command.Name), ex);
                    }

                    if (cmd.GetType() == typeof(Commands.Use))
                    {
                        var useArgs = (Commands.UseArguments)args;
                        Guard.ArgumentExpressionTrueForCommand<Commands.Use>(() => useArgs.BrowserType.Count > 0);

                        browserList.AddRange(useArgs.BrowserType);
                    }
                    else
                    {
                        cmd.Execute(manager, args);
                    }
                }

                if (browserList.Count == 0)
                {
                    browserList.Add(BrowserType.Chrome);
                }

                manager.PlayWith(browserList.ToArray());
            }
            catch (FluentAutomation.API.Exceptions.AssertException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while executing the specified commands.", ex);
            }
            finally
            {
                provider.Cleanup();
            }
        }

        public static dynamic DeserializeArguments(Type type, Dictionary<string, string> arguments)
        {
            var result = Activator.CreateInstance(type);

            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var argumentKey = arguments.Keys.FirstOrDefault(x => x.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));

                if (argumentKey != null)
                {
                    var value = arguments[argumentKey];

                    // Nullable properties
                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        var args = property.PropertyType.GetGenericArguments();

                        // Nullable<Enum>
                        if (args.First().IsEnum)
                        {
                            property.SetValue(result, Enum.Parse(args.First(), value), null);
                        }
                        // Nullable<int>
                        if (args.First().Name == typeof(Int32).Name)
                        {
                            property.SetValue(result, Int32.Parse(value), null);
                        }
                    }
                    // Handle expressions
                    else if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Expression<>))
                    {
                        var args = property.PropertyType.GetGenericArguments();
                        var firstArg = args.First();

                        // Expression<Func<>>
                        if (firstArg.IsGenericType && firstArg.GetGenericTypeDefinition() == typeof(Func<>))
                        {
                            var argArgs = firstArg.GetGenericArguments();

                            // Expression<Func<string, bool>>
                            if (argArgs.Length == 2 &&
                                argArgs[0].GetType() == typeof(string) &&
                                argArgs[1].GetType() == typeof(bool))
                            {
                                // figure out how to dynamically parse expressions here... VS2008 DynamicExpressions.. Flee.. something.
                            }
                        }
                    }
                    // Handle normal enumerations
                    else if (property.PropertyType.IsEnum)
                    {
                        property.SetValue(result, Enum.Parse(property.PropertyType, value), null);
                    }
                    // string -> API.Point
                    else if (property.PropertyType == typeof(API.Point))
                    {
                        API.Point point = new API.Point()
                        {
                            X = Int32.Parse(value.Substring(0, value.IndexOf(','))),
                            Y = Int32.Parse(value.Substring(value.IndexOf(',') + 1))
                        };

                        property.SetValue(result, point, null);
                    }
                    // string -> API.Size
                    else if (property.PropertyType == typeof(API.Size))
                    {
                        API.Size size = new API.Size()
                        {
                            Width = Int32.Parse(value.Substring(0, value.IndexOf(','))),
                            Height = Int32.Parse(value.Substring(value.IndexOf(',') + 1))
                        };

                        property.SetValue(result, size, null);
                    }
                    // Handle IConvertible types
                    else
                    {
                        property.SetValue(result, Convert.ChangeType(value, property.PropertyType), null);
                    }
                }
            }

            return result;
        }
    }
}
