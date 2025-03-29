using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static FillYourColors.MainWindow;


namespace FillYourColors;

public partial class MainWindow : Window
{    
    private int nameStakPanel = 0;
    private const short maxCollor = 5;
    private List<ColorItem> colorCheck = new List<ColorItem>();

    public class ColorItem
    {
        public string Text { get; set; }
        public SolidColorBrush Color { get; set; }
        public ColorItem(string text, SolidColorBrush color)
        {
            Text = text;
            Color = color;
        }
        public ColorItem() 
        { }
    }
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        byte alpha , red, green, blue;
        if (alphaCheck.IsChecked == true)
            alpha = (byte)A_Slider.Value;
        else
            alpha = (byte)255;
        if (redCheck.IsChecked == true)
            red = (byte)R_Slider.Value;
        else
            red = (byte)0;
        if (greenCheck.IsChecked == true)
            green = (byte)G_Slider.Value;
        else
            green = (byte)0;
        if (blueCheck.IsChecked == true)
            blue = (byte)B_Slider.Value;
        else
            blue = (byte)0;

        Color color = Color.FromArgb(alpha, red, green, blue);
        SolidColorBrush brush = new SolidColorBrush(color);
        ListColor.Background = brush;
        LockButton();
    }

    private ListBox CreateListBox(ListBox listBox, ColorItem colorItem)
    {
        const int marginListBox = 5;
        listBox.Background = colorItem.Color;
        listBox.Width = 550;
        listBox.Height = 27;
        listBox.Margin = new Thickness(marginListBox);
        return listBox;
    }

    private TextBox CreateTextBox(TextBox newTextBox, ColorItem colorItem)
    {
        const int marginTextBox = 5;
        newTextBox.Text = colorItem.Text;
        newTextBox.Width = 70;
        newTextBox.Height = 27;
        newTextBox.Margin = new Thickness(marginTextBox);
        return newTextBox;
    }

    private Button CreateDellButton(Button delButton)
    {
        delButton.Content = "Delete";
        delButton.Width = 40;
        delButton.Height = 35;
        delButton.Click+= DeleteButton_Click;
        return delButton;
    }

    private void newColorButton_Click(object sender, RoutedEventArgs e)
    {
        
        SolidColorBrush scb = (SolidColorBrush)ListColor.Background;        
        ColorItem colorItem = new ColorItem();
        StackPanel createStec = new StackPanel();
        TextBox newTextBox = new TextBox();
        ListBox listBox = new ListBox();
        Button delButton = new Button();

        colorItem.Color = scb;
        colorItem.Text = ListColor.Background.ToString();

        colorCheck.Add(colorItem);
        delButton.Tag = colorItem;

        CreateTextBox(newTextBox, colorItem);
        CreateListBox(listBox, colorItem);
        CreateDellButton(delButton);

        createStec.Orientation = Orientation.Horizontal;
        stacPanelOsnova.Children.Add(createStec);
        createStec.Children.Add(newTextBox);
        createStec.Children.Add(listBox);
        createStec.Children.Add(delButton);
        nameStakPanel++;
        LockButton();
    }
    private void LockButton()
    {
        if (colorCheck.Count == 0)
        {
            newColorButton.IsEnabled = true;
        }
        foreach (var c in colorCheck)
            if (c.Text == ListColor.Background.ToString())
                newColorButton.IsEnabled = false;
            else
                newColorButton.IsEnabled = true;
            
    }
    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {        
        if (MessageBox.Show("Вы уверены, что хотите удалить панель?", "Подтверждение",
        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            Button button = (Button)sender;
            TextBox tb = new TextBox(); 
            StackPanel parentStackPanel = (StackPanel)button.Parent;
            StackPanel mainStackPanel = (StackPanel)parentStackPanel.Parent;
            ColorItem cm = new ColorItem();
            foreach (var c in colorCheck)
            {
                if(c == button.Tag)
                    cm = c;
            }
            colorCheck.Remove(cm);
            mainStackPanel.Children.Remove(parentStackPanel);
            nameStakPanel--;
            LockButton();
        }
    }
}