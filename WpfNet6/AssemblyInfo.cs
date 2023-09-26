using System.Windows;
using System.Windows.Markup;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]

[assembly: XmlnsDefinition("http://CustomUI/ResourceToolKit/2023/xaml", "WpfNet6.CommonResource")]
[assembly: XmlnsDefinition("http://CustomUI/ResourceToolKit/2023/xaml", "WpfNet6.CommonUi.AttachedProperty")]
[assembly: XmlnsDefinition("http://CustomUI/ResourceToolKit/2023/xaml", "WpfNet6.CommonUi.Behavior")]
[assembly: XmlnsDefinition("http://CustomUI/ResourceToolKit/2023/xaml", "WpfNet6.CommonUi.BindingProxy")]
[assembly: XmlnsDefinition("http://CustomUI/ResourceToolKit/2023/xaml", "WpfNet6.CommonUi.Converters")]
[assembly: XmlnsDefinition("http://CustomUI/ResourceToolKit/2023/xaml", "WpfNet6.CommonUi.UserControl")]
[assembly: XmlnsDefinition("http://CustomUI/ResourceToolKit/2023/xaml", "WpfNet6.CommonUi.Validation")]

[assembly: XmlnsPrefix("http://CustomUI/ResourceToolKit/2023/xaml", "tool")]
