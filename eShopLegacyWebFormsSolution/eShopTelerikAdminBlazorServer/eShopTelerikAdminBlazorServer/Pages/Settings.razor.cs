using System;
using System.Collections.Generic;

using eShopTelerikAdminBlazorServer.Models;

using Microsoft.AspNetCore.Components.Forms;

namespace eShopTelerikAdminBlazorServer.Pages
{
    public partial class Settings
    {
        public Settings()
        {

        }
        int selectedValue { get; set; }
        bool switchValue { get; set; } = true;

        private DateTime min = new DateTime(2015, 1, 1);
        private DateTime max = new DateTime(2025, 12, 31);
        private DateTime theDate { get; set; } = DateTime.Now;
        private string selectedDate = "";
        public bool WindowVisible { get; set; }

        private void MyValueChangeHandler(DateTime newValue)
        {
            selectedDate = newValue.ToString("dd MMM yyyy");
        }
        public void WindowButtonClicked()
        {
            navManager.NavigateTo("/", true);
        }

        SettingsModel settingsModel { get; set; } = new SettingsModel()
        {
            Username = "JaxonWhite",
            Nickname = "Jax",
            Email = "jaxon.white@gmail.com",
            Phone = "(+1) 8373-837-93-02",
            Website = "jxnss.com",
            Country = new List<MyDropDownListModel>
            {
            new MyDropDownListModel() { MyValueField = 1, MyTextField = "USA"},
            new MyDropDownListModel() { MyValueField = 2, MyTextField = "Bulgaria"},
            new MyDropDownListModel() { MyValueField = 3, MyTextField = "Argentina"}
        },

        };

        public EditContext? EditContext { get; set; }

        protected override void OnInitialized()
        {
            EditContext = new EditContext(settingsModel);
            selectedValue = 3;
            base.OnInitialized();
        }
    }
}
