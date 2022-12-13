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
namespace AsepriteDotNet.MonoGame.Image;

public class SpritesheetAnimation
{
    /// <summary>
    ///     Gets the collection of the <see cref="SpritesheetFrame"/> elements
    ///     that make up this <see cref="SpritesheetAnimation"/> in order from
    ///     start to end.
    /// </summary>
    public List<SpritesheetFrame> Frames { get; } = new();

    /// <summary>
    ///     Gets or Sets the name of this <see cref="SpritesheetAnimation"/>.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Gets or Sets the <see cref="AnimationDirection"/> that should be
    ///     used when using this <see cref="SpritesheetAnimation"/>.
    /// </summary>
    public AnimationDirection Direction { get; set; } = AnimationDirection.Forward;

    /// <summary>
    ///     Gets or Sets a value that indicates whether this
    ///     <see cref="SpritesheetAnimation"/> loops.
    /// </summary>
    public bool IsLooping { get; set; } = true;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SpritesheetAnimation"/>
    ///     class.
    /// </summary>
    /// <param name="name">
    ///     The name of this <see cref="SpritesheetAnimation"/>.
    /// </param>
    public SpritesheetAnimation(string name) => Name = name;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SpritesheetAnimation"/>
    ///     class.
    /// </summary>
    /// <param name="name">
    ///     The name of this <see cref="SpritesheetAnimation"/>.
    /// </param>
    /// <param name="frames">
    ///     The <see cref="SpritesheetFrame"/> elements that make up this
    ///     <see cref="SpritesheetAnimation"/>, in order from start to end.
    /// </param>
    public SpritesheetAnimation(string name, List<SpritesheetFrame> frames)
        : this(name) => Frames = frames;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SpritesheetAnimation"/>
    ///     class.
    /// </summary>
    /// <param name="name">
    ///     The name of this <see cref="SpritesheetAnimation"/>.
    /// </param>
    /// <param name="frames">
    ///     The <see cref="SpritesheetFrame"/> elements that make up this
    ///     <see cref="SpritesheetAnimation"/>, in order from start to end.
    /// </param>
    /// <param name="direction">
    ///     The <see cref="AnimationDirection"/> that should be used when using
    ///     this <see cref="SpritesheetAnimation"/>.
    /// </param>
    public SpritesheetAnimation(string name, List<SpritesheetFrame> frames, AnimationDirection direction)
        : this(name, frames) => Direction = direction;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SpritesheetAnimation"/>
    ///     class.
    /// </summary>
    /// <param name="name">
    ///     The name of this <see cref="SpritesheetAnimation"/>.
    /// </param>
    /// <param name="frames">
    ///     The <see cref="SpritesheetFrame"/> elements that make up this
    ///     <see cref="SpritesheetAnimation"/>, in order from start to end.
    /// </param>
    /// <param name="direction">
    ///     The <see cref="AnimationDirection"/> that should be used when using
    ///     this <see cref="SpritesheetAnimation"/>.
    /// </param>
    /// <param name="isLooping">
    ///     Whether this <see cref="SpritesheetAnimation"/> should loop.
    /// </param>
    public SpritesheetAnimation(string name, List<SpritesheetFrame> frames, AnimationDirection direction, bool isLooping)
        : this(name, frames, direction) => IsLooping = isLooping;
}