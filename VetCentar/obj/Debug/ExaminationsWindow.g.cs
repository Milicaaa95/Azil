﻿#pragma checksum "..\..\ExaminationsWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EDFAF413AAE25BD6BF29E2E37DCD0366"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using VetCentar;
using VetCentar.Properties;


namespace VetCentar {
    
    
    /// <summary>
    /// ExaminationsWindow
    /// </summary>
    public partial class ExaminationsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\ExaminationsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid resultsTable;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\ExaminationsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addNewResult;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\ExaminationsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button takeMedicine;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\ExaminationsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button detailsButton;
        
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
            System.Uri resourceLocater = new System.Uri("/VetCentar;component/examinationswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ExaminationsWindow.xaml"
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
            this.resultsTable = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.addNewResult = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\ExaminationsWindow.xaml"
            this.addNewResult.Click += new System.Windows.RoutedEventHandler(this.AddNewResult_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.takeMedicine = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\ExaminationsWindow.xaml"
            this.takeMedicine.Click += new System.Windows.RoutedEventHandler(this.TakeMedicine_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.detailsButton = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\ExaminationsWindow.xaml"
            this.detailsButton.Click += new System.Windows.RoutedEventHandler(this.DetailsButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

