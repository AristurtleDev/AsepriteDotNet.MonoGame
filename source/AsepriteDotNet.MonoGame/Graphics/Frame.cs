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
///     Represents a frame within a <see cref="Atlas"/>.
/// </summary>
public class Frame
{
    private Dictionary<string, List<Slice>> _sliceLookup = new();

    /// <summary>
    ///     Gets or Sets the bounds of this <see cref="Frame"/> within the 
    ///     <see cref="Atlas"/>.
    /// </summary>
    public Rectangle SourceRectangle { get; set; } = Rectangle.Empty;

    /// <summary>
    ///     Gets the duration of this <see cref="Frame"/> when used in an 
    ///     animation.
    /// </summary>
    public TimeSpan Duration { get; set; } = TimeSpan.Zero;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Frame"/> class.
    /// </summary>
    /// <param name="sourceRectangle">
    ///     The bounds of this <see cref="Frame"/> within a 
    ///     <see cref="Atlas"/>.
    /// </param>
    public Frame(Rectangle sourceRectangle) => SourceRectangle = sourceRectangle;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Frame"/> class.
    /// </summary>
    /// <param name="sourceRectangle">
    ///     The bounds of this <see cref="Frame"/> within a 
    ///     <see cref="Atlas"/>.
    /// </param>
    /// <param name="duration">
    ///     THe duration of this <see cref="Frame"/> when used in an animation.
    /// </param>
    public Frame(Rectangle sourceRectangle, TimeSpan duration)
        : this(sourceRectangle) => Duration = duration;

    /// <summary>
    ///     Adds the given <see cref="Slice"/> to this <see cref="Frame"/>.
    /// </summary>
    /// <param name="slice">
    ///     The <see cref="Slice"/> to add.
    /// </param>
    public void AddSlice(Slice slice)
    {
        if (_sliceLookup.ContainsKey(slice.Name))
        {
            _sliceLookup[slice.Name].Add(slice);
        }
        else
        {
            _sliceLookup.Add(slice.Name, new List<Slice>() { slice });
        }
    }

    /// <summary>
    ///     Adds each <see cref="Slice"/> element in the given collection
    ///     to this <see cref="Frame"/>.
    /// </summary>
    /// <param name="slices">
    ///     The collection containing the <see cref="Slice"/> elements to
    ///     add.
    /// </param>
    public void AddSlices(IEnumerable<Slice> slices)
    {
        foreach (Slice slice in slices)
        {
            AddSlice(slice);
        }
    }

    /// <summary>
    ///     Removes the given <see cref="Slice"/> element from this
    ///     <see cref="Frame"/>.
    /// </summary>
    /// <param name="slice">
    ///     The <see cref="Slice"/> element to remove.
    /// </param>
    public void RemoveSlice(Slice slice)
    {
        if (_sliceLookup.ContainsKey(slice.Name))
        {
            List<Slice> frameSlices = _sliceLookup[slice.Name];
            frameSlices.Remove(slice);

            if (frameSlices.Count == 0)
            {
                _sliceLookup.Remove(slice.Name);
            }
        }
    }

    /// <summary>
    ///     Removes all <see cref="Slice"/> elements with the specified
    ///     <paramref name="name"/> from this <see cref="Frame"/>.
    /// </summary>
    /// <param name="name"></param>
    public void RemoveSlicesByName(string name) => _sliceLookup.Remove(name);

    /// <summary>
    ///     Removes each <see cref="Slice"/> element in the given
    ///     collection from this <see cref="Frame"/>.
    /// </summary>
    /// <param name="slices">
    ///     The collection of <see cref="Slice"/> elements to remove.
    /// </param>
    public void RemoveSlices(IEnumerable<Slice> slices)
    {
        foreach (Slice slice in slices)
        {
            RemoveSlice(slice);
        }
    }

    /// <summary>
    ///     Returns a new collection of all <see cref="Slice"/> elements
    ///     from this <see cref="Frame"/>.
    /// </summary>
    /// <returns>
    ///     A new collection of all <see cref="Slice"/> elements from this 
    ///     <see cref="Frame"/>.
    /// </returns>
    public List<Slice> GetAllSlices()
    {
        List<Slice> slices = new();

        foreach (var slice in _sliceLookup)
        {
            slices.AddRange(slice.Value);
        }

        return slices;
    }

    /// <summary>
    ///     Returns a new collection of all <see cref="Slice"/> elements
    ///     with the specified <paramref name="name"/> from this
    ///     <see cref="Frame"/>.
    /// </summary>
    /// <param name="name">
    ///     The name of the <see cref="Slice"/> elements to return.
    /// </param>
    /// <returns>
    ///     A new collection of all <see cref="Slice"/> elements with the
    ///     specified <paramref name="name"/> from this 
    ///     <see cref="Frame"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if the specified <paramref name="name"/> is
    ///     <see langword="null"/> or an empty string.
    /// </exception>
    public List<Slice> GetSlicesByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name), $"{nameof(name)} cannot be null or an empty string");
        }

        if (_sliceLookup.TryGetValue(name, out List<Slice>? slices))
        {
            return slices;
        }

        return new List<Slice>();
    }

    /// <summary>
    ///     Returns the first occurrence of a <see cref="Slice"/> element
    ///     with the specified <paramref name="name"/> from this
    ///     <see cref="Frame"/>.
    /// </summary>
    /// <param name="name">
    ///     The name of the <see cref="Slice"/> element to return.
    /// </param>
    /// <returns>
    ///     The first occurrence of a <see cref="Slice"/> element with the
    ///     specified <paramref name="name"/> from this
    ///     <see cref="Atlas"/>, if one is found; otherwise, 
    ///     <see langword="null"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if the specified <paramref name="name"/> is
    ///     <see langword="null"/> or an empty string.
    /// </exception>
    public Slice? GetFirstSliceWithName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name), $"{nameof(name)} cannot be null or an empty string");
        }

        return GetSlicesByName(name).FirstOrDefault();
    }
}