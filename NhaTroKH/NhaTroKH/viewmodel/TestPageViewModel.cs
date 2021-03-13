using NhaTroKH.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NhaTroKH.ViewModel
{
    public class TestPageViewModel
    {

        private ObservableCollection<Employee> employeeCollection;
        public ObservableCollection<Employee> EmployeeCollection
        {
            get { return employeeCollection; }
            set { employeeCollection = value; }
        }
        public TestPageViewModel()
        {

            employeeCollection = new ObservableCollection<Employee>();
            employeeCollection.Add(new Employee() { Name = "Frank" });
            employeeCollection.Add(new Employee() { Name = "James" });
            employeeCollection.Add(new Employee() { Name = "Steve" });
            employeeCollection.Add(new Employee() { Name = "Lucas" });
            employeeCollection.Add(new Employee() { Name = "Mark" });
            employeeCollection.Add(new Employee() { Name = "Mark" });
            employeeCollection.Add(new Employee() { Name = "Mark" });
            employeeCollection.Add(new Employee() { Name = "Mark" });
            employeeCollection.Add(new Employee() { Name = "Mark" });
            employeeCollection.Add(new Employee() { Name = "Mark" });

        }
    }
}
