﻿#pragma checksum "C:\Users\Tomek\documents\visual studio 2013\Projects\Rezerwacja\Rezerwacja\Rezerwacje.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "094B61C359F2219E7E6F18C8C59A064A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Rezerwacja {
    
    
    public partial class Rezerwacje : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.LongListSelector ListaRezerwacje;
        
        internal Microsoft.Phone.Controls.LongListSelector ListaPrzyszlychRezerwacji;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Rezerwacja;component/Rezerwacje.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ListaRezerwacje = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("ListaRezerwacje")));
            this.ListaPrzyszlychRezerwacji = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("ListaPrzyszlychRezerwacji")));
        }
    }
}

