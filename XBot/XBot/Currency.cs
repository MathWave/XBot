using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XBot
{
    public class Currency : ContentPage
    {
        Picker entry = new Picker();

        string t = "\nВалюта\n";

        public Currency()
        {
            if (Device.RuntimePlatform == "iOS")
            {
                Title = "Валюта";
                t = "";
            }
            else
                NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Styles.BackColor;
            MakeContent();
        }

        void MakeContent()
        {
            StackLayout sl = new StackLayout();
            Button button1 = new Button
            {
                Text = "⊕",
                FontSize = 20,
                TextColor = Xamarin.Forms.Color.Green,
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Styles.BackColor,
                BorderColor = Styles.UserColor,
                HorizontalOptions = LayoutOptions.Start,
            };
            button1.Clicked += Add;
            entry = new Picker
            {
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TextColor = Styles.UserColor,
                BackgroundColor = Styles.BackColor,
                Title = "Добавить валюту"
            };
            List<string> subs = Formats.FromStringIntoList((string)App.Current.Properties["currency"]);
            foreach (string c in CurrencyNum.Keys)
                if (!subs.Contains(c))
                    entry.Items.Add(c);
            Frame newframe = new Frame
            {
                BorderColor = Styles.UserColor,
                Content = new StackLayout
                {
                    Children =
                    {
                        button1,
                        entry
                    },
                    Orientation = StackOrientation.Horizontal
                },
                BackgroundColor = Styles.BackColor
            };
            for (int i = subs.Count - 1; i >= 0; i--)
            {
                Button button = new Button
                {
                    Text = "⊖",
                    FontSize = 20,
                    ClassId = i.ToString(),
                    TextColor = Xamarin.Forms.Color.Red,
                    FontAttributes = FontAttributes.Bold,
                    BorderColor = Styles.UserColor,
                    HorizontalOptions = LayoutOptions.Start,
                    BackgroundColor = Styles.BackColor
                };
                button.Clicked += (object sender, EventArgs e) =>
                {
                    subs.RemoveAt(int.Parse(((Button)sender).ClassId));
                    App.Current.Properties["currency"] = Formats.FromListIntoString(subs);
                    MakeContent();
                };
                Frame newf = new Frame
                {
                    BorderColor = Styles.UserColor,
                    BackgroundColor = Styles.BackColor,
                    Content = new StackLayout
                    {
                        Children =
                        {
                            button,
                            new Label
                            {
                                Text = subs[i],
                                TextColor = Styles.UserColor,
                                HorizontalTextAlignment = TextAlignment.Start,
                                VerticalTextAlignment = TextAlignment.Center
                            }
                        },
                        Orientation = StackOrientation.Horizontal
                    }
                };
                sl.Children.Add(newf);
            }
            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = t,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 20,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.End,
                        TextColor = Styles.UserColor,
                        FontAttributes = FontAttributes.Bold,
                        BackgroundColor = Styles.BackColor
                    },
                    newframe,
                    new ScrollView { Content = sl }
                }
            };
        }

        void Add(object sender, EventArgs e)
        {
            List<string> subs = Formats.FromStringIntoList((string)App.Current.Properties["currency"]);
            if (entry.SelectedItem != null && ((string)entry.SelectedItem).Length != 0)
            {
                subs.Add((string)entry.SelectedItem);
                App.Current.Properties["currency"] = Formats.FromListIntoString(subs);
                MakeContent();
            }
        }

        public static Dictionary<string, int> CurrencyNum = new Dictionary<string, int>
        {
            {"Австралийский доллар", 12},
            {"Азербайджанский манат", 21},
            {"Фунт стерлингов Соединенного королевства", 30},
            {"Армянский драм", 39},
            {"Белорусский рубль", 48},
            {"Болгарский лев", 57},
            {"Бразильский реал", 66},
            {"Венгерский форинт", 75},
            {"Гонконгский доллар", 84},
            {"Датская крона", 93},
            {"Доллар США", 102},
            {"Евро", 111},
            {"Индийская рупия", 120},
            {"Казахстанский тенге", 129},
            {"Канадский доллар", 138},
            {"Киргизский сом", 147},
            {"Китайский юань", 156},
            {"Молдавский лей", 165},
            {"Норвежская крона", 174},
            {"Польский злотый", 183},
            {"Румынский лей", 192},
            {"Сингапурский доллар", 210},
            {"Таджикский сомони", 219},
            {"Турецкая лира", 228},
            {"Новый туркменский манат", 237},
            {"Узбекский сум", 246},
            {"Украинская гривна", 255},
            {"Чешская крона", 264},
            {"Шведская крона", 273},
            {"Швейцарский франк", 282},
            {"Южноафриканский рэнд", 291},
            {"Вон Республики Корея", 300},
            {"Японская иена", 309}
        };

        public static Dictionary<string, string> CurrencyId = new Dictionary<string, string>
        {
            {"Австралийский доллар", "AUD"},
            {"Азербайджанский манат", "AZN"},
            {"Фунт стерлингов Соединенного королевства", "GBP"},
            {"Армянский драм", "AMD"},
            {"Белорусский рубль", "BYN"},
            {"Болгарский лев", "BGN"},
            {"Бразильский реал", "BRL"},
            {"Венгерский форинт", "HUF"},
            {"Гонконгский доллар", "HKD"},
            {"Датская крона", "DKK"},
            {"Доллар США", "USD"},
            {"Евро", "EUR"},
            {"Индийская рупия", "INR"},
            {"Казахстанский тенге", "KZT"},
            {"Канадский доллар", "CAD"},
            {"Киргизский сом", "KGS"},
            {"Китайский юань", "CNY"},
            {"Молдавский лей", "MDL"},
            {"Норвежская крона", "NOK"},
            {"Польский злотый", "PLN"},
            {"Румынский лей", "RON"},
            {"Сингапурский доллар", "SGD"},
            {"Таджикский сомони", "TJS"},
            {"Турецкая лира", "TRY"},
            {"Новый туркменский манат", "TMT"},
            {"Узбекский сум", "UZS"},
            {"Украинская гривна", "UAH"},
            {"Чешская крона", "CZK"},
            {"Шведская крона", "SEK"},
            {"Швейцарский франк", "CHF"},
            {"Южноафриканский рэнд", "ZAR"},
            {"Вон Республики Корея", "KRW"},
            {"Японская иена", "JPY"}
        };
    }
}

