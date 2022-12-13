/* -----------------------------------------------------------------------------
Copyright 2022 Christopher Whitley

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
----------------------------------------------------------------------------- */
using System.ComponentModel;

// using AsepriteDotNet.Image;
using AsepriteDotNet.MonoGame.Image;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace AsepriteDotNet.MonoGame.Pipeline.Processors;

public sealed class AsepriteSheetProcessor : ContentProcessor<AsepriteFile, AsepriteDotNet.Image.AsepriteSheet>
{
    /// <summary>
    ///     Gets or Sets the <see cref="PackingMethod"/> to use when generating
    ///     images data from the <see cref="AsepriteFile"/>.
    /// </summary>
    /// <remarks>
    ///     This applies to <see cref="Spritesheet"/> and 
    ///     <see cref="Tilesheet"/> image data generation.
    /// </remarks>
    [DisplayName("Packing Method")]
    public AsepriteDotNet.Image.PackingMethod PackingMethod { get; set; } = AsepriteDotNet.Image.PackingMethod.SquarePacked;

    /// <summary>
    ///     Gets or Sets a value that indicates if color values should be 
    ///     premultiplied.
    /// </summary>
    [DisplayName("Premultiply Alpha?")]
    public bool PremultiplyAlpha { get; set; } = true;

    /// <summary>
    ///     Gets or Sets a value that indicates whether duplicate image data
    ///     for frames and/or tiles should be merged into a single frame/tile
    ///     when generating image data from the <see cref="AsepriteFile"/>.
    /// </summary>
    /// <remarks>
    ///     This applies to <see cref="Spritesheet"/> and 
    ///     <see cref="Tilesheet"/> image data generation.
    /// </remarks>
    [DisplayName("Merge Duplicate Frames?")]
    public bool MergeDuplicates { get; set; } = true;

    /// <summary>
    ///     Gets or Sets a value that indicates if only cels that are on
    ///     visible layers should be included when generating image data
    ///     from the <see cref="AsepriteFile"/>.
    /// </summary>
    /// <remarks>
    ///     This applies to <see cref="Spritesheet"/> image data generation
    ///     only.
    /// </remarks>
    [DisplayName("Only Visible Layers?")]
    public bool OnlyVisibleLayers { get; set; } = true;

    /// <summary>
    ///     Gets or Sets the amount of transparent pixels to add between each
    ///     frame or tile and the edge of the sheet when generating image data 
    ///     from the <see cref="AsepriteFile"/>.
    /// </summary>
    /// <remarks>
    ///     This applies to <see cref="Spritesheet"/> and 
    ///     <see cref="Tilesheet"/> image data generation.
    /// </remarks>
    [DisplayName("Border Padding")]
    public int BorderPadding { get; set; } = 0;

    /// <summary>
    ///     Gets or Sets the amount of transparent pixels to add to the inside
    ///     of each frame or tile in the the sheet when generating image data
    ///     from the <see cref="AsepriteFile"/>.
    /// </summary>
    /// <remarks>
    ///     This applies to <see cref="Spritesheet"/> and 
    ///     <see cref="Tilesheet"/> image data generation.
    /// </remarks>
    [DisplayName("Inner Padding")]
    public int InnerPadding { get; set; } = 0;

    /// <summary>
    ///     Gets or Sets the amount of transparent pixels to add between each
    ///     frame or tile in the sheet when generating image data from the
    ///     <see cref="AsepriteFile"/>.
    /// </summary>
    /// <remarks>
    ///     This applies to <see cref="Spritesheet"/> and 
    ///     <see cref="Tilesheet"/> image data generation.
    /// </remarks>
    [DisplayName("Spacing")]
    public int Spacing { get; set; } = 0;

    /// <summary>
    ///     Processes the <see cref="AsepriteFile"/> to prepare it for writing.
    /// </summary>
    /// <remarks>
    ///     This method overload is intended to be used when performing the
    ///     pipeline import/process/write without the use of the content
    ///     pipeline tool.
    /// </remarks>
    /// <param name="input">
    ///     The <see cref="AsepriteFile"/> that was created by the 
    ///     <see cref="Importers.AsepriteFileImporter"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="AsepriteSheet"/> class instance 
    /// </returns>
    public AsepriteDotNet.Image.AsepriteSheet Process(AsepriteFile input) => Process(input, null);
    public override AsepriteDotNet.Image.AsepriteSheet Process(AsepriteFile input, ContentProcessorContext? context)
    {
        AsepriteDotNet.Image.SpritesheetOptions sOptions = new()
        {
            OnlyVisibleLayers = OnlyVisibleLayers,
            MergeDuplicates = MergeDuplicates,
            PackingMethod = PackingMethod,
            BorderPadding = BorderPadding,
            Spacing = Spacing,
            InnerPadding = InnerPadding
        };

        AsepriteDotNet.Image.TilesheetOptions tOptions = new()
        {
            MergeDuplicates = MergeDuplicates,
            PackingMethod = PackingMethod,
            BorderPadding = BorderPadding,
            Spacing = Spacing,
            InnerPadding = InnerPadding
        };

        AsepriteDotNet.Image.AsepriteSheet aseSheet = input.ToAsepriteSheet(sOptions, tOptions);

        Spritesheet spritesheet = CreateSpritesheet(aseSheet);

        return input.ToAsepriteSheet(sOptions, tOptions);

    }

    private Spritesheet CreateSpritesheet(AsepriteDotNet.Image.AsepriteSheet aseSheet)
    {
        //  Color values need to be translated from AsepriteDotNet color to 
        //  MonoGame color values
        Color[] sheetPixels = new Color[aseSheet.Spritesheet.Pixels.Count];

        for (int i = 0; i < sheetPixels.Length; i++)
        {
            var pixel = aseSheet.Spritesheet.Pixels[i];
            if (PremultiplyAlpha)
            {
                sheetPixels[i] = Color.FromNonPremultiplied(pixel.R, pixel.G, pixel.B, pixel.A);
            }
            else
            {
                sheetPixels[i] = new Color(pixel.R, pixel.G, pixel.B, pixel.A);
            }
        }

        //  Translate frame data
        List<SpritesheetFrame> frames = new();

        foreach (var aseFrame in aseSheet.Spritesheet.Frames)
        {
            List<FrameSlice> slices = new();

            foreach (var aseSlice in aseFrame.GetSlices())
            {
                string name = aseSlice.Name;
                Rectangle bounds = new Rectangle(aseSlice.Bounds.X, aseSlice.Bounds.Y, aseSlice.Bounds.Width, aseSlice.Bounds.Height);

                Rectangle? center = default;
                if (aseSlice.CenterBounds is not null)
                {
                    center = new Rectangle(aseSlice.CenterBounds.Value.X, aseSlice.CenterBounds.Value.Y, aseSlice.CenterBounds.Value.Width, aseSlice.CenterBounds.Value.Height);
                }

                Point? pivot = default;
                if (aseSlice.Pivot is not null)
                {
                    pivot = new Point(aseSlice.Pivot.Value.X, aseSlice.Pivot.Value.Y);
                }

                slices.Add(new(name, bounds, center, pivot));
            }
        }
    }
}