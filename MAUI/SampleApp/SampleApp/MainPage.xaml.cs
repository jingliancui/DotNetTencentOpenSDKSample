using CommunityToolkit.Mvvm.Messaging;
using SampleApp.Messages;

namespace SampleApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void ShareBtn_Clicked(object sender, EventArgs e)
    {        
        WeakReferenceMessenger.Default.Send(new ShareMessage());
    }
}

