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
using UnityEngine.SceneManagement;




public class OpenBrowse : MonoBehaviour
{
	DBManager DBManager;

	GetAndSetText GATS;

	public static string ObtainedID;

	string text;


	void Start()
	{

		DBManager = gameObject.GetComponent<DBManager>();

		if((SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 6) && PlayerPrefs.GetInt("LastScene") != 4)
		{
			SetID();
		}
			
	}
		

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
		
	}
    
    IEnumerator Browse(string filetype, string estensione)
    {

    	FileBrowser.SetFilters( true, new FileBrowser.Filter( filetype, estensione ));

		FileBrowser.SetDefaultFilter( estensione );

		FileBrowser.SetExcludedExtensions( ".lnk", ".tmp", ".zip", ".rar", ".exe" );
		
		FileBrowser.AddQuickLink( "Users", "C:\\Users", null );

		// Show a save file dialog 
		// onSuccess event: not registered (which means this dialog is pretty useless)
		// onCancel event: not registered
		// Save file/folder: file, Initial path: "C:\", Title: "Save As", submit button text: "Save"
		// FileBrowser.ShowSaveDialog( null, null, false, "C:\\", "Save As", "Save" );
		FileBrowser.SingleClickMode = true;

		yield return StartCoroutine( ShowLoadDialogCoroutine() );
	    		
    }

    IEnumerator FotoUP()
    {
    	yield return StartCoroutine(Browse("Photos", ".jpg"));
    	string nomefile = Upload(text, "files.000webhost.com", "napolioutdoorams", "ApaMondaSpaziani", "/public_html/foto");
    	DBManager.InserisciFoto(nomefile);
    }

    IEnumerator VideoUP()
    {
        yield return StartCoroutine(Browse("Videos", ".mp4"));
        string nomefile = Upload(text, "files.000webhost.com", "napolioutdoorams", "ApaMondaSpaziani", "/public_html/video");
        DBManager.InserisciVideo(nomefile);

    }


    IEnumerator AudioITAUP()
    {
 		yield return StartCoroutine(Browse("Audios", ".ogg"));
        string nomefile = Upload(text, "files.000webhost.com", "napolioutdoorams", "ApaMondaSpaziani", "/public_html/audio");
        DBManager.ModificaAudioITA(nomefile);   	
    }

    IEnumerator AudioENGUP()
    {
 		yield return StartCoroutine(Browse("Audios", ".ogg"));
        string nomefile = Upload(text, "files.000webhost.com", "napolioutdoorams", "ApaMondaSpaziani", "/public_html/audio");
        DBManager.ModificaAudioENG(nomefile);   	
    }

    IEnumerator AudioFRAUP()
    {
 		yield return StartCoroutine(Browse("Audios", ".ogg"));
        string nomefile = Upload(text, "files.000webhost.com", "napolioutdoorams", "ApaMondaSpaziani", "/public_html/audio");
        DBManager.ModificaAudioFRA(nomefile);   	
    }





    public void VideoUpload()
	{
			
		if(int.Parse(ObtainedID) < 0)
    	{
    		Debug.Log("No POI inserted!");
    		DBManager.VariableText.text = "No POI inserted! You can't add videos!";
    	}
    	else
    	{
    		StartCoroutine(VideoUP());
		}
	}


	public void AudioITAUpload()
	{
			
		if(int.Parse(ObtainedID) < 0)
    	{
    		Debug.Log("No POI inserted!");
    		DBManager.VariableText.text = "No POI inserted! You can't add audios!";	
    	}
    	else
    	{	
			StartCoroutine(AudioITAUP());
		}
	}

	public void AudioENGUpload()
	{
			
		if(int.Parse(ObtainedID) < 0)
    	{
    		Debug.Log("No POI inserted!");
    		DBManager.VariableText.text = "No POI inserted! You can't add audios!";
    	}
    	else
    	{	
			StartCoroutine(AudioENGUP());
		}
	}


	public void AudioFRAUpload()
	{
			
		if(int.Parse(ObtainedID) < 0)
    	{
    		Debug.Log("No POI inserted!");
    		DBManager.VariableText.text = "No POI inserted! You can't add audios!";
    	}
    	else
    	{	
			StartCoroutine(AudioFRAUP());
		}
	}



	public void FotoUpload()
	{
			
		if(int.Parse(ObtainedID) < 0)
    	{
    		Debug.Log("No POI inserted!");
    		DBManager.VariableText.text = "No POI inserted! You can't add videos!";
    		
    	}
    	else
    	{	
			StartCoroutine(FotoUP());
		}
	}



	/// <summary>
	/// Uploads a file through FTP.
	/// </summary>
	/// <param name="filename">The path to the file to upload.</param>
	/// <param name="server">The server to use.</param>
	/// <param name="username">The username to use.</param>
	/// <param name="password">The password to use.</param>
	/// <param name="initialPath">The path on the server to upload to.</param>
	static string Upload (string filename, string server, string username, string password, string initialPath)
	{
		var file = new FileInfo(filename);
		var address = new Uri("ftp://" + server + "/" + Path.Combine(initialPath, file.Name));
		var request = FtpWebRequest.Create(address) as FtpWebRequest;

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
			return "";
		}
			

		Debug.Log("Upload successful.");

		return file.Name;
	}

	public void SetID()
	{
		ObtainedID = "-1";
	}

}
