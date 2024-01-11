using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ArabicLettersMatchingGame.Models;

namespace ArabicLettersMatchingGame.Services;

public abstract class GetTextPairsStrategy
{
    // Need to move 3 folders up from debug C# executable
    private readonly string _jsonFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../../../Assets/ArabicScipt.json"));
    protected FileStream? JsonFs;
    
    // variable for getting random number
    protected Random Rng = new (10);
    
     // Rules with managing _jsonFS.
     // Use OpenJsonFile to start using _jsonFS.
     // Once done with the file, make sure to close the file.
     // Making variable null is not needed as long as you call OpenJsonFile before you start using file.

    
     /**
      * Makes a List of random TextPairs that can be used by the game views as text to show on the cards.
      */
     public abstract List<TextPair> GetRandomPairs();

    /**
     * Opens the json file and returns a JsonDocument. Does not close the variable.
     */
    protected JsonElement OpenJsonFile()
    {
        // no need to try to open if already open
        if (JsonFs is { CanRead: true }) return JsonDocument.Parse(JsonFs).RootElement;
        
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


        return JsonDocument.Parse(JsonFs).RootElement;
    }

    /**
     * Gets letters[i] from the json file and returns as a string[]. Closes the file after use.
     */
    protected string[] GetLetterArray(int i)
    {
        // first open the json file
        var jsonRoot = OpenJsonFile();
        
        // need to do a check on i
        var letterLen = jsonRoot.GetProperty("len_letters").GetUInt16();
        
        // if i is out of bounds, then put it to nearest boundary index
        if (i < 0) i = 0;
        if (i >= letterLen) i = letterLen-1;
        
        // no need to check as json element exists cause file is made that way
        var letterArrayElement = jsonRoot.GetProperty("letters")[i];
        
        // now deserialize it as a string[]
        var letterArray = JsonSerializer.Deserialize<string[]>(letterArrayElement!.GetRawText());
        
        // close file as done with it now
        JsonFs!.Close();
        
        return letterArray!;
    }

    /**
     * Gets words[i] from the json file and returns as a string[]. Closes the file after use.
     */
    protected string[] GetWordArray(int i)
    {
        // first open the json file
        var jsonRoot = OpenJsonFile();
        
        // need to do a check on i
        var wordLen = jsonRoot.GetProperty("len_words").GetUInt16();
        
        // if i is out of bounds, then put it to nearest boundary index
        if (i < 0) i = 0;
        if (i >= wordLen) i = wordLen-1;
        
        // no need to check as json element exists cause file is made that way
        var wordArrayElement = jsonRoot.GetProperty("words")[i]; 
        
        // now deserialize it as a string[]
        var wordArray = JsonSerializer.Deserialize<string[]>(wordArrayElement!.GetRawText());
        
        // close file as done with it now
        JsonFs!.Close();
        
        return wordArray!;
    }

    /**
     * Factory function to create a TextPair given an array of texts, and two indexes.
     * i1 and i2 have to be within the boundary of texts.
     */
    protected static TextPair CreateTextPairFromArray(string[] texts, int i1, int i2)
    {
        return new TextPair(texts[i1], texts[i2]);
    }
}

