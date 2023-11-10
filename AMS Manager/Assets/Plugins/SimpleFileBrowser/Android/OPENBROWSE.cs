using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFileBrowser;
using System;
using System.IO;
using System.Net;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Networking;





public class OPENBROWSE : MonoBehaviour
	{
		public InputField Name;
		
		
		string text;

		IEnumerator ShowLoadDialogCoroutine()
		{
		// Show a load file dialog and wait for a response from user
		// Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
		yield return FileBrowser.WaitForLoadDialog( false, null, "Load File", "Load" );

		// Dialog is closed
		// Print whether a file is chosen (FileBrowser.Success)
		// and the path to the selected file (FileBrowser.Result) (null, if FileBrowser.Success is false)
		Debug.Log( FileBrowser.Success + " " + FileBrowser.Result );

		text = FileBrowser.Result;

		VideoUpload();
	
		
		
		}
    
    public void Browse()
    {

    	if(String.IsNullOrEmpty(Name.text))
    	{
    		Debug.Log("No POI inserted before video selection!");
    	}
    	else
    	{
    		FileBrowser.SetFilters( true, new FileBrowser.Filter( "Videos", ".mp4" )/*, new FileBrowser.Filter( "Text Files", ".txt", ".pdf" )*/ );

		// Set default filter that is selected when the dialog is shown (optional)
		// Returns true if the default filter is set successfully
		// In this case, set Images filter as the default filter
		FileBrowser.SetDefaultFilter( ".mp4" );

		// Set excluded file extensions (optional) (by default, .lnk and .tmp extensions are excluded)
		// Note that when you use this function, .lnk and .tmp extensions will no longer be
		// excluded unless you explicitly add them as parameters to the function
		FileBrowser.SetExcludedExtensions( ".lnk", ".tmp", ".zip", ".rar", ".exe" );
		
		// Name: Users
		// Path: C:\Users
		// Icon: default (folder icon)
		FileBrowser.AddQuickLink( "Users", "C:\\Users", null );

		// Show a save file dialog 
		// onSuccess event: not registered (which means this dialog is pretty useless)
		// onCancel event: not registered
		// Save file/folder: file, Initial path: "C:\", Title: "Save As", submit button text: "Save"
		// FileBrowser.ShowSaveDialog( null, null, false, "C:\\", "Save As", "Save" );

		// Coroutine example
		FileBrowser.SingleClickMode = true;
		StartCoroutine( ShowLoadDialogCoroutine() );


		
    	}	
    }



    public void VideoUpload()
	{
		Upload(text, "files.000webhost.com", "napolioutdoorams", "ApaMondaSpaziani", "/public_html/video");
	}

	/// <summary>
	/// Uploads a file through FTP.
	/// </summary>
	/// <param name="filename">The path to the file to upload.</param>
	/// <param name="server">The server to use.</param>
	/// <param name="username">The username to use.</param>
	/// <param name="password">The password to use.</param>
	/// <param name="initialPath">The path on the server to upload to.</param>
	static void Upload (string filename, string server, string username, string password, string initialPath)
	{
		var file = new FileInfo(filename);
		var address = new Uri("ftp://" + server + "/" + Path.Combine(initialPath, file.Name));
		var request = FtpWebRequest.Create(address) as FtpWebRequest;

		// Upload options:

		// Provide credentials
		request.Credentials = new NetworkCredential(username, password);

		// Set control connection to closed after command execution
		request.KeepAlive = false;

		// Specify command to be executed
		request.Method = WebRequestMethods.Ftp.UploadFile;

		// Specify data transfer type
		request.UseBinary = true;

		// Notify server about size of uploaded file
		request.ContentLength = file.Length;

		// Set buffer size to 2KB.
		var bufferLength = 2048;
		var buffer = new byte[bufferLength];
		var contentLength = 0;

		// Open file stream to read file
		var fs = file.OpenRead();

		try {
			// Stream to which file to be uploaded is written.
			var stream = request.GetRequestStream();

			// Read from file stream 2KB at a time.
			contentLength = fs.Read(buffer, 0, bufferLength);

			// Loop until stream content ends.
			while (contentLength != 0) {
				//Debug.Log("Progress: " + ((fs.Position / fs.Length) * 100f));
				// Write content from file stream to FTP upload stream.
				stream.Write(buffer, 0, contentLength);
				contentLength = fs.Read(buffer, 0, bufferLength);
			}

			// Close file and request streams
			stream.Close();
			fs.Close();
		} catch (Exception e) {
			Debug.LogError("Error uploading file: " + e.Message);
			return;
		}

		Debug.Log("Upload successful.");


	}

	

	


	
}
