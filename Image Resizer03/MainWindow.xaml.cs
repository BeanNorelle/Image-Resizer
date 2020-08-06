
using System;
using System.Windows;

using System.Drawing;

using System.IO;

using System.Text.RegularExpressions;

using System.Windows.Input;




namespace Image_Resizer03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    /// <summary>
    /// Method to resize and save the image.
    /// </summary>
    /// <param name="image">Bitmap image.</param>
    /// <param name="maxWidth">resize width.</param>
    /// <param name="maxHeight">resize height.</param>
    /// <param name="filePath">file path.</param> 
    public partial class MainWindow : Window
    {

        int NewWidth = 55, NewHeight = 55;
        string sSelectedFile;
        string sSelectedFolder;
        int fcount = 0, i = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region dragmove 
        private void Borderform_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        #endregion


        #region event handler got focus for import and export text boxees
        private void StackPanel_GotFocus(object sender, RoutedEventArgs e)
        {


            imptxt.Visibility = Visibility.Hidden;

            if (String.IsNullOrEmpty(importtxt.Text))
            {
                //imptxt.Visibility = Visibility.Visible;

                System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
                //fbd.Description = "Custom Description"; //not mandatory

                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    sSelectedFolder = fbd.SelectedPath;
                else
                    sSelectedFolder = string.Empty;

                importtxt.Text = sSelectedFolder;

                //fcount = Directory.GetFiles(importtxt.Text, "*", SearchOption.AllDirectories).Length;
                //exptxt.Text = (Convert.ToString(fcount));
            }

        }


        private void textblk_MouseDown(object sender, MouseButtonEventArgs e)
        {

            imptxt.Visibility = Visibility.Hidden;
            importtxt.Focus();


        }
        private void textblk2_MouseDown(object sender, MouseButtonEventArgs e)
        {

            exptxt.Visibility = Visibility.Hidden;
            exporttxt.Focus();

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Importtxt_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            imptxt.Visibility = Visibility.Hidden;


        }
        private void Importtxt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(importtxt.Text))
            {
                imptxt.Visibility = Visibility.Visible;

            }
        }

        private void Exporttxt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(exporttxt.Text))
            {
                exptxt.Visibility = Visibility.Visible;

            }
        }

        private void Exporttxt_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {


            exptxt.Visibility = Visibility.Hidden;


        }
        #endregion

        #region Close
        private void Close_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Close_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Space)
            {
                this.Close();
            }
        }

        private void Importtxt_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            imptxt.Visibility = Visibility.Hidden;

            if (String.IsNullOrEmpty(importtxt.Text))
            {
                //imptxt.Visibility = Visibility.Visible;

                System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
                //fbd.Description = "Custom Description"; //not mandatory

                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    sSelectedFolder = fbd.SelectedPath;
                else
                    sSelectedFolder = string.Empty;

                importtxt.Text = sSelectedFolder;

                //fcount = Directory.GetFiles(importtxt.Text, "*", SearchOption.AllDirectories).Length;
                //exptxt.Text = (Convert.ToString(fcount));
            }
        }
        #endregion

        #region button resize
        private void Resize_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(importtxt.Text))
            {
                MessageBox.Show("Select image location");
            }
            if (string.IsNullOrEmpty(exporttxt.Text))
            {
                MessageBox.Show("Select image location");
            }
            else
            {
                try
                {
                    Resizee();
                }
                catch
                {

                }
            }
         
           
        }

        private void StackPanel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Space)
            {
                Resizee();
            }
        }
        #endregion


        #region resize button event
        private void Resizee()
        {

         
                    DirectoryInfo directory = new DirectoryInfo(importtxt.Text);
                    string[] filePaths = Directory.GetFiles(importtxt.Text);
                    if (filePaths.Length > 0)
                    {
                        FileInfo[] files = directory.GetFiles();
                        ResizeImages(files);
                        MessageBox.Show("Done!");
                    }
    
        }
        #endregion

        #region resize method
        private void ResizeImages(FileInfo[] files)
        {


            int h = Convert.ToInt32(exporttxt.Text);
            int w = h;

                foreach (FileInfo file in files)
                {
                        Image img = Image.FromFile(file.FullName);
                        var newImage = ScaleImage(img, h, w);
                            string path2 = @"C:\Users\Bean Norelle\Pictures\New folder";
                        string imagepath = file.FullName + path2;
                        newImage.Save(importtxt.Text + "\\" + "Resized" + "_" + exporttxt.Text + ".pixels" + "_" + file.Name);
                        i = i + 1;

                        img.Dispose();
                        newImage.Dispose();

                }
        }
        #endregion

        #region scale image
        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }

        #endregion

        //public void Save(Bitmap image, int maxWidth, int maxHeight, int quality, string filePath)
        //    {
        //        // Get the image's original width and height
        //        int originalWidth = image.Width;
        //        int originalHeight = image.Height;

        //        // To preserve the aspect ratio
        //        float ratioX = (float)maxWidth / (float)originalWidth;
        //        float ratioY = (float)maxHeight / (float)originalHeight;
        //        float ratio = Math.Min(ratioX, ratioY);

        //        // New width and height based on aspect ratio
        //        int newWidth = (int)(originalWidth * ratio);
        //        int newHeight = (int)(originalHeight * ratio);

        //        // Convert other formats (including CMYK) to RGB.
        //        Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

        //        // Draws the image in the specified size with quality mode set to HighQuality
        //        using (Graphics graphics = Graphics.FromImage(newImage))
        //        {
        //            graphics.CompositingQuality = CompositingQuality.HighQuality;
        //            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //            graphics.SmoothingMode = SmoothingMode.HighQuality;
        //            graphics.DrawImage(image, 0, 0, newWidth, newHeight);
        //        }

        //        // Get an ImageCodecInfo object that represents the JPEG codec.
        //        ImageCodecInfo imageCodecInfo = this.GetEncoderInfo(ImageFormat.Jpeg);

        //        // Create an Encoder object for the Quality parameter.
        //        Encoder encoder = Encoder.Quality;

        //        // Create an EncoderParameters object. 
        //        EncoderParameters encoderParameters = new EncoderParameters(1);

        //        // Save the image as a JPEG file with quality level.
        //        EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
        //        encoderParameters.Param[0] = encoderParameter;
        //        newImage.Save(filePath, imageCodecInfo, encoderParameters);
        //    }

        //private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        //{
        //    return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        //}
    }
   
}
    
