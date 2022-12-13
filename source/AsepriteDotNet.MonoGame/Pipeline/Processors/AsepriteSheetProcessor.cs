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

using AsepriteDotNet.Image;

using Microsoft.Xna.Framework.Content.Pipeline;

namespace AsepriteDotNet.MonoGame.Pipeline.Processors;

public sealed class AsepriteSheetProcessor : ContentProcessor<AsepriteFile, AsepriteSheet>
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
    public PackingMethod PackingMethod { get; set; } = PackingMethod.SquarePacked;

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
    public AsepriteSheet Process(AsepriteFile input) => Process(input, null);
    public override AsepriteSheet Process(AsepriteFile input, ContentProcessorContext? context)
    {
        SpritesheetOptions sOptions = new()
        {
            OnlyVisibleLayers = OnlyVisibleLayers,
            MergeDuplicates = MergeDuplicates,
            PackingMethod = PackingMethod,
            BorderPadding = BorderPadding,
            Spacing = Spacing,
            InnerPadding = InnerPadding
        };

        TilesheetOptions tOptions = new()
        {
            MergeDuplicates = MergeDuplicates,
            PackingMethod = PackingMethod,
            BorderPadding = BorderPadding,
            Spacing = Spacing,
            InnerPadding = InnerPadding
        };

        return input.ToAsepriteSheet(sOptions, tOptions);
    }
}