using System.ComponentModel;

namespace My_Note
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            car.Content = new Views.NoteView();
            // car.content=new Views.NoteView();
        }

        
    }

}
