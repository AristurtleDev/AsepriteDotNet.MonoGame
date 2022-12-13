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
using Microsoft.Xna.Framework;

namespace AsepriteDotNet.MonoGame.Graphics;

/// <summary>
///     Represents a defined rectangular area within a <see cref="Frame"/>.
/// </summary>
public class Slice
{
    /// <summary>
    ///     Gets or Sets the bounds of this <see cref="Slice"/> relative to the
    ///     bounds of the <see cref="Frame"/> it is in.
    /// </summary>
    public Rectangle Bounds { get; set; } = Rectangle.Empty;

    /// <summary>
    ///     Gets or Sets the bounds of the center rectangle for this
    ///     <see cref="Slice"/> if it is a 9-patches slice; otherwise,
    ///     <see langword="null"/>.
    /// </summary>
    public Rectangle? CenterBounds { get; set; } = default;

    /// <summary>
    ///     Gets or Sets the pivot point for this <see cref="Slice"/> if it has
    ///     a pivot point; otherwise, <see langword="null"/>.
    /// </summary>
    public Point? Pivot { get; set; } = default;

    /// <summary>
    ///     Gets or Sets the name of this <see cref="Slice"/>.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Gets or Sets the color of this <see cref="Slice"/>.
    /// </summary>
    public Color Color { get; set; } = Color.Blue;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Slice"/> class.
    /// </summary>
    /// <param name="name">
    ///     The name of this <see cref="Slice"/>.
    /// </param>
    public Slice(string name) => Name = name;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Slice"/> class.
    /// </summary>
    /// <param name="name">
    ///     The name of this <see cref="Slice"/>.
    /// </param>
    /// <param name="bounds">
    ///     The bounds of this <see cref="Slice"/> relative to the bounds of the
    ///     <see cref="Frame"/> it is in.
    /// </param>
    public Slice(string name, Rectangle bounds) => (Name, Bounds) = (name, bounds);

    /// <summary>
    ///     Initializes a new instance of the <see cref="Slice"/> class.
    /// </summary>
    /// <param name="name">
    ///     The name of this <see cref="Slice"/>.
    /// </param>
    /// <param name="bounds">
    ///     The bounds of this <see cref="Slice"/> relative to the bounds of the
    ///     <see cref="Frame"/> it is in.
    /// </param>
    /// <param name="centerBounds">
    ///     The bounds of the center rectangle if this is a 9-patches slice;
    ///     otherwise, <see langword="null"/>.
    /// </param>
    /// <param name="pivot">
    ///     The pivot point for this <see cref="Slice"/> if it has a pivot
    ///     point; otherwise, <see langword="null"/>.
    /// </param>
    public Slice(string name, Rectangle bounds, Rectangle? centerBounds = default, Point? pivot = default) =>
        (Name, Bounds, CenterBounds, Pivot) = (name, bounds, centerBounds, pivot);
}