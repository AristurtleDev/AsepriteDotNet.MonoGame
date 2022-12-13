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

namespace AsepriteDotNet.MonoGame.Image;

/// <summary>
///     Represents a frame within a <see cref="Spritesheet"/>.
/// </summary>
public class SpritesheetFrame
{
    private Dictionary<string, List<FrameSlice>> _sliceLookup = new();

    /// <summary>
    ///     Gets or Sets the bounds of this <see cref="SpritesheetFrame"/>
    ///     within the <see cref="Spritesheet"/>.
    /// </summary>
    public Rectangle SourceRectangle { get; set; } = Rectangle.Empty;

    /// <summary>
    ///     Gets the duration of this <see cref="SpritesheetFrame"/> when used
    ///     in an animation.
    /// </summary>
    public TimeSpan Duration { get; set; } = TimeSpan.Zero;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SpritesheetFrame"/>
    ///     class.
    /// </summary>
    public SpritesheetFrame() { }

    /// <summary>
    ///     Adds the given <see cref="FrameSlice"/> to this
    ///     <see cref="SpritesheetFrame"/>.
    /// </summary>
    /// <param name="slice">
    ///     The <see cref="FrameSlice"/> to add.
    /// </param>
    public void AddSlice(FrameSlice slice)
    {
        if (_sliceLookup.ContainsKey(slice.Name))
        {
            _sliceLookup[slice.Name].Add(slice);
        }
        else
        {
            _sliceLookup.Add(slice.Name, new List<FrameSlice>() { slice });
        }
    }

    /// <summary>
    ///     Adds each <see cref="FrameSlice"/> element in the given collection
    ///     to this <see cref="SpritesheetFrame"/>.
    /// </summary>
    /// <param name="slices">
    ///     The collection containing the <see cref="FrameSlice"/> elements to
    ///     add.
    /// </param>
    public void AddSlices(IEnumerable<FrameSlice> slices)
    {
        foreach (FrameSlice slice in slices)
        {
            AddSlice(slice);
        }
    }

    /// <summary>
    ///     Removes the given <see cref="FrameSlice"/> element from this
    ///     <see cref="SpritesheetFrame"/>.
    /// </summary>
    /// <param name="slice">
    ///     The <see cref="FrameSlice"/> element to remove.
    /// </param>
    public void RemoveSlice(FrameSlice slice)
    {
        if (_sliceLookup.ContainsKey(slice.Name))
        {
            List<FrameSlice> frameSlices = _sliceLookup[slice.Name];
            frameSlices.Remove(slice);

            if (frameSlices.Count == 0)
            {
                _sliceLookup.Remove(slice.Name);
            }
        }
    }

    /// <summary>
    ///     Removes all <see cref="FrameSlice"/> elements with the specified
    ///     <paramref name="name"/> from this <see cref="SpritesheetFrame"/>.
    /// </summary>
    /// <param name="name"></param>
    public void RemoveSlicesByName(string name) => _sliceLookup.Remove(name);

    /// <summary>
    ///     Removes each <see cref="FrameSlice"/> element in the given
    ///     collection from this <see cref="SpritesheetFrame"/>.
    /// </summary>
    /// <param name="slices">
    ///     The collection of <see cref="FrameSlice"/> elements to remove.
    /// </param>
    public void RemoveSlices(IEnumerable<FrameSlice> slices)
    {
        foreach (FrameSlice slice in slices)
        {
            RemoveSlice(slice);
        }
    }

    /// <summary>
    ///     Returns a new collection of all <see cref="FrameSlice"/> elements
    ///     from this <see cref="SpritesheetFrame"/>.
    /// </summary>
    /// <returns>
    ///     A new collection of all <see cref="FrameSlice"/> elements from this 
    ///     <see cref="SpritesheetFrame"/>.
    /// </returns>
    public List<FrameSlice> GetAllSlices()
    {
        List<FrameSlice> slices = new();

        foreach (var slice in _sliceLookup)
        {
            slices.AddRange(slice.Value);
        }

        return slices;
    }

    /// <summary>
    ///     Returns a new collection of all <see cref="FrameSlice"/> elements
    ///     with the specified <paramref name="name"/> from this
    ///     <see cref="SpritesheetFrame"/>.
    /// </summary>
    /// <param name="name">
    ///     The name of the <see cref="FrameSlice"/> elements to return.
    /// </param>
    /// <returns>
    ///     A new collection of all <see cref="FrameSlice"/> elements with the
    ///     specified <paramref name="name"/> from this 
    ///     <see cref="SpritesheetFrame"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if the specified <paramref name="name"/> is
    ///     <see langword="null"/> or an empty string.
    /// </exception>
    public List<FrameSlice> GetSlicesByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name), $"{nameof(name)} cannot be null or an empty string");
        }

        if (_sliceLookup.TryGetValue(name, out List<FrameSlice>? slices))
        {
            return slices;
        }

        return new List<FrameSlice>();
    }

    /// <summary>
    ///     Returns the first occurrence of a <see cref="FrameSlice"/> element
    ///     with the specified <paramref name="name"/> from this
    ///     <see cref="SpritesheetFrame"/>.
    /// </summary>
    /// <param name="name">
    ///     The name of the <see cref="FrameSlice"/> element to return.
    /// </param>
    /// <returns>
    ///     The first occurrence of a <see cref="FrameSlice"/> element with the
    ///     specified <paramref name="name"/> from this
    ///     <see cref="Spritesheet"/>, if one is found; otherwise, 
    ///     <see langword="null"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if the specified <paramref name="name"/> is
    ///     <see langword="null"/> or an empty string.
    /// </exception>
    public FrameSlice? GetFirstSliceWithName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name), $"{nameof(name)} cannot be null or an empty string");
        }

        return GetSlicesByName(name).FirstOrDefault();
    }
}