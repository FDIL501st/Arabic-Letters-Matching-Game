using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ArabicLettersMatchingGame.Services;

public abstract class GetTextPairsStrategy
{
    private const string JsonFilePath = "../Assets/ArabicScript.json";
    private FileStream? _jsonFs;
    
    /**
     * Rules with managing _jsonFS.
     * Use OpenJsonFile to start using _jsonFS.
     * Once done with the file, make sure to close the file.
     * Making variable null is not needed as long as you call OpenJsonFile before you start using file.
     */

    /**
     * Opens the json file and returns a JsonDocument. Does not close the variable.
     */
    private JsonDocument OpenJsonFile()
    {
        try
        {
            // first open json file as a filestream
            _jsonFs = File.OpenRead(JsonFilePath);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
        return JsonDocument.Parse(_jsonFs);
    }

    /**
     * Returns letters from json file.
     */
    protected JsonData GetArabicLetters()
    {
        // first need JsonDocument object
        var jsonDoc = OpenJsonFile();
        
        // check to make sure json file is open as function relies on that
        if (_jsonFs == null) return new JsonData();
        
        
            
        _jsonFs.Close();

        throw new NotImplementedException("GetJsonLetters() is not implemented.");
    }

    /**
     * Returns words from json file.
     */
    protected JsonData GetArabicWords()
    {
        throw new NotImplementedException("GetJsonWords() is not implemented.");
    }
}

public class JsonData
{
    public string[][]? Texts { get; set; }
}
