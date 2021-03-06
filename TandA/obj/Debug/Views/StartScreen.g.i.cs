﻿#pragma checksum "..\..\..\Views\StartScreen.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "558472BE8EC1CAD22B45EB8A669AE712"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Syncfusion;
using Syncfusion.SfSkinManager;
using Syncfusion.UI.Xaml.Controls.DataPager;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.UI.Xaml.Grid.RowFilter;
using Syncfusion.UI.Xaml.TreeGrid;
using Syncfusion.Windows;
using Syncfusion.Windows.Controls.Notification;
using Syncfusion.Windows.Shared;
using Syncfusion.Windows.Tools;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TandA.Views;


namespace TandA.Views {
    
    
    /// <summary>
    /// StartScreen
    /// </summary>
    public partial class StartScreen : Syncfusion.Windows.Tools.Controls.RibbonWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\Views\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Syncfusion.Windows.Tools.Controls.Ribbon _ribbon;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Views\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Syncfusion.Windows.Tools.Controls.RibbonBar Employees;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Views\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Syncfusion.Windows.Tools.Controls.RibbonBar Groups;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Views\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Syncfusion.Windows.Tools.Controls.RibbonBar Absenthiesm;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TandA;component/views/startscreen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\StartScreen.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this._ribbon = ((Syncfusion.Windows.Tools.Controls.Ribbon)(target));
            return;
            case 2:
            this.Employees = ((Syncfusion.Windows.Tools.Controls.RibbonBar)(target));
            return;
            case 3:
            
            #line 23 "..\..\..\Views\StartScreen.xaml"
            ((Syncfusion.Windows.Tools.Controls.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.Add_Employee_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 24 "..\..\..\Views\StartScreen.xaml"
            ((Syncfusion.Windows.Tools.Controls.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.Employee_List_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Groups = ((Syncfusion.Windows.Tools.Controls.RibbonBar)(target));
            return;
            case 6:
            
            #line 29 "..\..\..\Views\StartScreen.xaml"
            ((Syncfusion.Windows.Tools.Controls.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.Add_Group_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 30 "..\..\..\Views\StartScreen.xaml"
            ((Syncfusion.Windows.Tools.Controls.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.Group_List_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Absenthiesm = ((Syncfusion.Windows.Tools.Controls.RibbonBar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

