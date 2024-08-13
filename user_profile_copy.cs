using System;
using System.IO;
using System.Management;

public class CopyFolderToUserProfile
{
    public static void Main(string[] args)
    {
        // Source folder path (temp directory)
        string sourceFolder = @"C:\temp\MyFolder";

        // Get the user profiles
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_UserProfile");
        ManagementObjectCollection userProfiles = searcher.Get();
		string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name; 
		Console.WriteLine(userName);
		
		string trimmedString = userName.TrimStart();
        Console.WriteLine(trimmedString); // Output: "This is a string with leading spaces.   "

        // Iterate through each user profile
        foreach (ManagementObject userProfile in userProfiles)
        {
            // Get the user profile path
            string userProfilePath = userProfile["LocalPath"].ToString();

            // Construct the destination folder path
            string destinationFolder = Path.Combine(userProfilePath, "MyFolder");

            // Copy the folder
            try
            {
                Directory.Copy(sourceFolder, destinationFolder, true);
                Console.WriteLine($"Copied folder to {destinationFolder}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error copying folder: {ex.Message}");
            }
        }
    }
}
