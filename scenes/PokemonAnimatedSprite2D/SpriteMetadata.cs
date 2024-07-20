using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Godot;
using System.Linq;

public struct Anim
{
    public string Name { get; set; }
    public int Index { get; set; }
    public int FrameWidth { get; set; }
    public int FrameHeight { get; set; }
    public int[] Durations { get; set; }
}
public struct AnimData
{
    public int ShadowSize { get; set; }
    public Anim[] Anims { get; set; }
}

public class SpriteMetadataLoader
{
    private const string SpriteResourceDirectory = "res://pokemon_data";
    public AnimData GetAnimData(string spriteDirectory)
    {
        // TODO: add caching via static property

        var path = $"{SpriteResourceDirectory}/{spriteDirectory}/AnimData.xml";

        string xmlContent;
        using (var file = FileAccess.Open(path, FileAccess.ModeFlags.Read))
        {
            xmlContent = file.GetAsText();
        }
        var doc = XDocument.Parse(xmlContent);
        var root = doc.Element("AnimData");
        return new AnimData
        {
            ShadowSize = int.Parse(root.Element("ShadowSize").Value),
            Anims = root.Element("Anims")
                .Elements("Anim")
                .Where(x => x.Element("CopyOf") == null)
                .Select(anim => new Anim
                {
                    Name = anim.Element("Name").Value,
                    Index = int.Parse(anim.Element("Index").Value),
                    FrameWidth = int.Parse(anim.Element("FrameWidth").Value),
                    FrameHeight = int.Parse(anim.Element("FrameHeight").Value),
                    Durations = anim.Element("Durations")
                        .Elements("Duration")
                        .Select(d => int.Parse(d.Value))
                        .ToArray()
                })
                .ToArray()
        };

    }

}