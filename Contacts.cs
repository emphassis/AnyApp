using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AnyApp
{
    public class Contacts
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string State { get; set; }

        public Contacts(string name, string phonenumber, string state)
        {
            Name = name;
            PhoneNumber = phonenumber;
            State = state;
        }
        public Contacts()
        {

        }
        public override string ToString()
        {
            return Name + " Phone#: " + PhoneNumber + " State: " + State;
        }
    }
}