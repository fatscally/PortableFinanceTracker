﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PFT.Pages
{
    /// <summary>
    /// Interaction logic for Suppliers.xaml
    /// </summary>
    public partial class Suppliers : Page
    {
        public Suppliers()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }
    }
}
