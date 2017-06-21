using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.IO;
using SQLite;
using Android.Content;
using System.Net;
using System;
using System.Collections.Specialized;
using System.Text;

namespace AnyApp
{
    [Activity(Label = "AnyApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        EditText nameEditText, phoneEditText, stateEditText;
        Button submitButton, getContactsButton, searchContactsButton, saveDatabaseBtn, act2Btn, sqlServerBtn;
        TextView contactsListTV;
        ProgressBar progress;
        List<Contacts> contactList = new List<Contacts>();
        string sqlPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Contact.db3");
        string contacts;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);
            nameEditText = FindViewById<EditText>(Resource.Id.NameET);
            phoneEditText = FindViewById<EditText>(Resource.Id.PhoneNumberET);
            stateEditText = FindViewById<EditText>(Resource.Id.stateET);
            submitButton = FindViewById<Button>(Resource.Id.submitBtn);
            act2Btn = FindViewById<Button>(Resource.Id.activity2Btn);
            searchContactsButton = FindViewById<Button>(Resource.Id.searchContactsBtn);
            saveDatabaseBtn = FindViewById<Button>(Resource.Id.saveDBBtn);
            getContactsButton = FindViewById<Button>(Resource.Id.getBtn);
            sqlServerBtn = FindViewById<Button>(Resource.Id.SQLServerBtn);
            contactsListTV = FindViewById<TextView>(Resource.Id.contactsTV);
            progress = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            submitButton.Click += SubmitButton_Click;
            getContactsButton.Click += GetContactsButton_Click;
            searchContactsButton.Click += SearchContactsButton_Click;
            saveDatabaseBtn.Click += SaveDatabaseBtn_Click;
            act2Btn.Click += Act2Btn_Click;
            sqlServerBtn.Click += SqlServerBtn_Click;

        }

        private void SqlServerBtn_Click(object sender, System.EventArgs e)
        {
            progress.Visibility = Android.Views.ViewStates.Visible;
            contactsListTV.Text = "Adding contacts to Database.";
            WebClient client = new WebClient();
            Uri uri = new Uri("http://192.168.0.50/CreateContact.php");
            NameValueCollection parameters = new NameValueCollection();
            foreach (var contact in contactList)
            {
                parameters.Clear();
                parameters.Add("Name", contact.Name);
                parameters.Add("Number", contact.PhoneNumber);
                parameters.Add("State", contact.State);
                client.UploadValues(uri, parameters);
            }
            progress.Visibility = Android.Views.ViewStates.Gone;
            contactsListTV.Text = "Contacts successfully added to SQL Server.";

        }

        private void Act2Btn_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Activity2));
            StartActivity(intent);
        }

        private void SaveDatabaseBtn_Click(object sender, System.EventArgs e)
        {
            var dbContacts = new SQLiteConnection(sqlPath);
            var contactsTable = dbContacts.Table<Contacts>();
            dbContacts.CreateTable<Contacts>();
            foreach (var contact in contactList)
            {
                dbContacts.Insert(contact);
            }
            dbContacts.Commit();
            contactsListTV.Text = "Database saved to SQLite.";
        }

        private void SearchContactsButton_Click(object sender, System.EventArgs e)
        {
            SearchContacts(nameEditText.Text, phoneEditText.Text, stateEditText.Text);
        }
        private void SearchContacts(string namestring, string numberstring, string statestring)
        {
            contactsListTV.Text = "";
            if (namestring != "")
            {
                foreach (var contact in contactList)
                {
                    if (contact.Name == namestring)
                    {
                        contactsListTV.Text = contactsListTV.Text + contact.ToString() + "\n";
                    }
                }
            }
            else if (numberstring != "")
            {
                foreach (var contact in contactList)
                {
                    if (contact.PhoneNumber == numberstring)
                    {
                        contactsListTV.Text = contactsListTV.Text + contact.ToString() + "\n";
                    }
                }
            }
            else if (statestring != "")
            {
                foreach (var contact in contactList)
                {
                    if (contact.State == statestring)
                    {
                        contactsListTV.Text = contactsListTV.Text + contact.ToString() + "\n";
                    }
                }
            }
        }


        private void GetContactsButton_Click(object sender, System.EventArgs e)
        {
            contactsListTV.Text = "";
            foreach (var contact in contactList)
            {
                contacts = contacts + contact.ToString() + "\n";
            }
            contactsListTV.Text = contacts;
        }

        private void SubmitButton_Click(object sender, System.EventArgs e)
        {
            string name = nameEditText.Text;
            string phone = phoneEditText.Text;
            string state = stateEditText.Text;
            Contacts contact = new Contacts(name, phone, state);
            contactList.Add(contact);
            nameEditText.Text = "";
            phoneEditText.Text = "";
            stateEditText.Text = "";
            contactsListTV.Text = "Contact has been added.";
        }

    }
}

