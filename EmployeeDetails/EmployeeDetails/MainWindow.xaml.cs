﻿using EmployeeDetails.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace EmployeeDetails
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();
        public MainWindow()
        {
            client.BaseAddress = new Uri("https://localhost:7110/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            InitializeComponent();
        }

        private void btnLoadEmployee_Click(object sender, RoutedEventArgs e)
        {
            this.GetEmployee();

        }
        private async void GetEmployee()
        {
            lblMessage.Content = "";
            var response = await client.GetStringAsync("employee");
            var employee = JsonConvert.DeserializeObject<List<Employee>>(response);
            dgEmployee.DataContext = employee;
        }


        private async void Save(Employee employee)
        {
            await client.PostAsJsonAsync("employee", employee);
        }

        private async void UpdateEmployee(Employee employee)
        {
            await client.PutAsJsonAsync("employee/"+ employee.EmployeeId, employee);
        }

        private async void DeleteEmployee(int EmployeeId)
        {
            await client.DeleteAsync("employee/"+ EmployeeId);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var employee = new Employee()
            {
                EmployeeId = Convert.ToInt32(txtEmployeeId.Text),
                Name = txtName.Text,
                Designation = txtDesignation.Text
            };

            if(employee.EmployeeId == 0)
            {
                this.Save(employee);
                lblMessage.Content = "Employee Details Saved Successfully !";
            }
            else
            {
                this.UpdateEmployee(employee);
                lblMessage.Content = "Employee Details Updated Successfully !";
            }

            txtEmployeeId.Text = 0.ToString();
            txtName.Text = "";
            txtDesignation.Text = "";
        }

        void btnEditEmployee(object sender, RoutedEventArgs e)
        {
            Employee employee = ((FrameworkElement)sender).DataContext as Employee;
            txtEmployeeId.Text=employee.EmployeeId.ToString();
            txtName.Text = employee.Name;
            txtDesignation.Text=employee.Designation;
        }

        void btnDeleteEmployee(object sender, RoutedEventArgs e)
        {
            Employee employee = ((FrameworkElement)sender).DataContext as Employee;
            this.DeleteEmployee(employee.EmployeeId);
        }
    }
}
