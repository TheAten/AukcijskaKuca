using AukcijskaKuca.Helpers;

namespace AukcijskaKuca.Model
{
    public class Product : PropertyChangedBase
    {

        public int Id
        {
            get;
            set;
        }

        private float price;

        public float Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

    }
}
