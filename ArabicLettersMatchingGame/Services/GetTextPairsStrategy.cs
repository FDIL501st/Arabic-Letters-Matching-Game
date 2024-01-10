using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ArabicLettersMatchingGame.Services;

public abstract class GetTextPairsStrategy
{
    // Need to move 3 folders up from debug C# executable
    private readonly string _jsonFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../../../Assets/ArabicScipt.json"));
    protected FileStream? JsonFs;
    
    /**
     * Rules with managing _jsonFS.
     * Use OpenJsonFile to start using _jsonFS.
     * Once done with the file, make sure to close the file.
     * Making variable null is not needed as long as you call OpenJsonFile before you start using file.
     */

    /**
     * Opens the json file and returns a JsonDocument. Does not close the variable.
     */
    protected JsonDocument OpenJsonFile()
    {
        try
        {
            // first open json file as a filestream
            JsonFs = File.OpenRead(_jsonFilePath);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
        return JsonDocument.Parse(JsonFs);
    }

    /**
     * Returns letters from json file.
     */
    protected JsonData GetArabicLetters()
    {
        // first need JsonDocument object
        var jsonDoc = OpenJsonFile();
        
        // check to make sure json file is open as function relies on that
        if (JsonFs == null) return new JsonData();
        
        
            
        JsonFs.Close();

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
