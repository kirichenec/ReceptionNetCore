using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Reception.App.ViewModels;
using System;

namespace Reception.App
{
    public class ViewLocator : IDataTemplate
    {
        public bool SupportsRecycling => false;

        public static MainWindowViewModel MainVM { get; set; }

        public IControl Build(object param)
        {
            var name = param.GetType().FullName.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type);
            }
            else
            {
                return new TextBlock { Text = "Not Found: " + name };
            }
        }

        public bool Match(object data)
        {
            return data is BaseViewModel;
        }
    }
}