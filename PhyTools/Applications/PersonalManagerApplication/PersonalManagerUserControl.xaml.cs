﻿using PhyTools.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhyTools.Applications.PersonalManagerApplication
{
    /// <summary>
    /// Interaction logic for PersonalManagerUserControl.xaml
    /// </summary>
    public partial class PersonalManagerUserControl : UserControl
    {
        public event PrintToConsole PrintToConsoleEvent;

        public PersonalManagerUserControl()
        {
            InitializeComponent();
        }
    }
}
