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
using SQLite;
using System.IO;

namespace AnyApp
{
    [Activity(Label = "Activity2")]
    public class Activity2 : Activity
    {
        EditText searchName, searchPhone, searchState;
        Button findContacts;
        TextView contactsList2;
        string sqlPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Contact.db3");
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Layout2);
            ActionBar.Hide();
            searchName = FindViewById<EditText>(Resource.Id.searchNameET);
            searchPhone = FindViewById<EditText>(Resource.Id.searchPhoneET);
            searchState = FindViewById<EditText>(Resource.Id.searchStateET);
            findContacts = FindViewById<Button>(Resource.Id.searchContact2Btn);
            contactsList2 = FindViewById<TextView>(Resource.Id.contactsListTV2);
            findContacts.Click += FindContacts_Click;
        }

        private void FindContacts_Click(object sender, EventArgs e)
        {
            if (searchName.Text == "" && searchPhone.Text == "" && searchState.Text == "")
            {

            }
            else
            {
                contactsList2.Text = "";
                var dbContacts = new SQLiteConnection(sqlPath);
                var contactsTable = dbContacts.Table<Contacts>();
                dbContacts.CreateTable<Contacts>();
                if (searchName.Text != "")
                {
                    foreach (var contact in contactsTable)
                    {
                        if (searchName.Text == contact.Name)
                        {
                            contactsList2.Text = contactsList2.Text + contact.ToString() + "\n";
                        }
                    }
                }
                else
                {

                    if (searchPhone.Text != "")
                    {
                        foreach (var contact in contactsTable)
                        {
                            if (searchPhone.Text == contact.PhoneNumber)
                            {
                                contactsList2.Text = contactsList2.Text + contact.ToString() + "\n";
                            }
                        }
                    }
                    else
                    {
                        if (searchState.Text != "")
                        {
                            foreach (var contact in contactsTable)
                            {
                                if (searchState.Text == contact.State)
                                {
                                    contactsList2.Text = contactsList2.Text + contact.ToString() + "\n";
                                }
                            }
                        }
                    }
                }
                
            }
        }
    }
}