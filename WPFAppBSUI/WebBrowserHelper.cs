using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFAppBSUI
{
    public class WebBrowserHelper
    {
        public static readonly DependencyProperty BindableSourceProperty =
            DependencyProperty.RegisterAttached("BindableSource", typeof(string), typeof(WebBrowserHelper), new UIPropertyMetadata(null, BindableSourcePropertyChanged));

        public static string GetBindableSource(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(BindableSourceProperty);
        }

        public static void SetBindableSource(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(BindableSourceProperty, value);
        }

        private static void BindableSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser webBrowser = d as WebBrowser;
            if (webBrowser != null)
            {
                string uri = e.NewValue as string;
                webBrowser.Source = !String.IsNullOrEmpty(uri) ? new Uri(uri) : null;
            }
           
        }
    }
}
