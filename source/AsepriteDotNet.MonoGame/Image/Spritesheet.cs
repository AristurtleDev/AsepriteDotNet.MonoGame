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
using Microsoft.Xna.Framework.Graphics;

namespace AsepriteDotNet.MonoGame.Image;

public class Spritesheet
{
    private Dictionary<string, List<SpritesheetAnimation>> _animations = new();

    /// <summary>
    ///     Gets the underlying <see cref="Texture2D"/> that represents the
    ///     full image of this <see cref="Spritesheet"/>.
    /// </summary>
    public Texture2D Texture { get; }

    /// <summary>
    ///     Get the width, in pixels, of this <see cref="Spritesheet"/>.
    /// </summary>
    public int Width => Texture.Height;

    /// <summary>
    ///     Gets the height, in pixels, of this <see cref="Spritesheet"/>.
    /// </summary>
    public int Height => Texture.Width;

    /// <summary>
    ///     Gets the collection of all <see cref="SpritesheetFrame"/> elements
    ///     in this <see cref="Spritesheet"/>.
    /// </summary>
    public List<SpritesheetFrame> Frames { get; } = new();

    /// <summary>
    ///     Initializes a new instance of the <see cref="Spritesheet"/> class.
    /// </summary>
    /// <param name="texture">
    ///     The <see cref="Texture2D"/> that is being represented by this
    ///     <see cref="Spritesheet"/>.
    /// </param>
    public Spritesheet(Texture2D texture) => Texture = texture;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Spritesheet"/> class.
    /// </summary>
    /// <param name="texture">
    ///     The <see cref="Texture2D"/> that is being represented by this
    ///     <see cref="Spritesheet"/>.
    /// </param>
    /// <param name="frames">
    ///     A collection of <see cref="SpritesheetFrame"/> elements that define
    ///     the frames within the <paramref name="texture"/>.
    /// </param>
    public Spritesheet(Texture2D texture, List<SpritesheetFrame> frames)
        : this(texture) => Frames = frames;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Spritesheet"/> class.
    /// </summary>
    /// <param name="texture">
    ///     The <see cref="Texture2D"/> that is being represented by this
    ///     <see cref="Spritesheet"/>.
    /// </param>
    /// <param name="frames">
    ///     A collection of <see cref="SpritesheetFrame"/> elements that define
    ///     the frames within the <paramref name="texture"/>.
    /// </param>
    /// <param name="animations">
    ///     A collection of <see cref="SpritesheetAnimation"/> elements that
    ///     define animations for this <see cref="Spritesheet"/>.
    /// </param>
    public Spritesheet(Texture2D texture, List<SpritesheetFrame> frames, List<SpritesheetAnimation> animations)
        : this(texture, frames)
    {
        AddAnimations(animations);
    }

    /// <summary>
    ///     Adds the given <see cref="SpritesheetAnimation"/> to this
    ///     <see cref="Spritesheet"/>.
    /// </summary>
    /// <param name="animation">
    ///     The <see cref="SpritesheetAnimation"/> to add.
    /// </param>
    public void AddAnimation(SpritesheetAnimation animation)
    {
        if (_animations.ContainsKey(animation.Name))
        {
            _animations[animation.Name].Add(animation);
        }
        else
        {
            _animations.Add(animation.Name, new List<SpritesheetAnimation>() { animation });
        }
    }

    /// <summary>
    ///     Adds a new <see cref="SpritesheetAnimation"/> to this 
    ///     <see cref="Spritesheet"/> with the specified <paramref name="name"/>
    ///     and <paramref name="frames"/>.
    /// </summary>
    /// <param name="name">
    ///     The name to give the <see cref="SpritesheetAnimation"/> that is
    ///     created.
    /// </param>
    /// <param name="frames">
    ///     The collection of <see cref="SpritesheetFrame"/> elements to add
    ///     to the <see cref="SpritesheetAnimation"/>, in order of start to end.
    /// </param>
    /// <returns>
    ///     The <see cref="SpritesheetAnimation"/> that is created by this
    ///     method.
    /// </returns>
    public SpritesheetAnimation AddAnimation(string name, List<SpritesheetFrame> frames, AnimationDirection direction = AnimationDirection.Forward, bool isLooping = true)
    {
        SpritesheetAnimation animation = new(name, frames, direction, isLooping);
        AddAnimation(animation);
        return animation;
    }

    /// <summary>
    ///     Adds each <see cref="SpritesheetAnimation"/> element in the given
    ///     collection to this <see cref="Spritesheet"/>.
    /// </summary>
    /// <param name="animations">
    ///     A collection containing the <see cref="SpritesheetAnimation"/>
    ///     elements to add.
    /// </param>
    public void AddAnimations(IEnumerable<SpritesheetAnimation> animations)
    {
        foreach (SpritesheetAnimation animation in animations)
        {
            AddAnimation(animation);
        }
    }

    /// <summary>
    ///     Removes the given <see cref="SpritesheetAnimation"/> element from
    ///     this <see cref="Spritesheet"/>.
    /// </summary>
    /// <param name="animation">
    ///     The <see cref="SpritesheetAnimation"/> element to remove.
    /// </param>
    public void RemoveAnimation(SpritesheetAnimation animation)
    {
        if (_animations.ContainsKey(animation.Name))
        {
            List<SpritesheetAnimation> animations = _animations[animation.Name];
            animations.Remove(animation);

            if (animations.Count == 0)
            {
                _animations.Remove(animation.Name);
            }
        }
    }

    /// <summary>
    ///     Removes all <see cref="SpritesheetAnimation"/> elements with the
    ///     specified <paramref name="name"/> from this
    ///     <see cref="Spritesheet"/>.
    /// </summary>
    /// <param name="name">
    ///     The name of the <see cref="SpritesheetAnimation"/> elements to
    ///     remove.
    /// </param>
    public void RemoveAnimationsByName(string name) => _animations.Remove(name);

    /// <summary>
    ///     Removes each <see cref="SpritesheetAnimation"/> element in the given
    ///     collection from this <see cref="Spritesheet"/>.
    /// </summary>
    /// <param name="animations"></param>
    public void RemoveAnimations(IEnumerable<SpritesheetAnimation> animations)
    {
        foreach (SpritesheetAnimation animation in animations)
        {
            RemoveAnimation(animation);
        }
    }

    /// <summary>
    ///     Returns a new collection of all <see cref="SpritesheetAnimation"/>
    ///     elements from this <see cref="Spritesheet"/>.
    /// </summary>
    /// <returns>
    ///     A new collection of all <see cref="SpritesheetAnimation"/> elements
    ///     from this <see cref="Spritesheet"/>.
    /// </returns>
    public List<SpritesheetAnimation> GetAllAnimations()
    {
        List<SpritesheetAnimation> animations = new();

        foreach (var animation in _animations)
        {
            animations.AddRange(animation.Value);
        }

        return animations;
    }

    /// <summary>
    ///     Returns a new collection of all <see cref="SpritesheetAnimation"/>
    ///     elements with the specified <paramref name="name"/> from this
    ///     <see cref="Spritesheet"/>.
    /// </summary>
    /// <param name="name">
    ///     The name of the <see cref="SpritesheetAnimation"/> elements to
    ///     return.
    /// </param>
    /// <returns>
    ///     A new collection of all <see cref="SpritesheetAnimation"/> elements
    ///     with the specified <paramref name="name"/> from this
    ///     <see cref="Spritesheet"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if the specified <paramref name="name"/> is
    ///     <see langword="null"/> or an empty string.
    /// </exception>
    public List<SpritesheetAnimation> GetAnimationsByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name), $"{nameof(name)} cannot be null or an empty string");
        }

        if (_animations.TryGetValue(name, out List<SpritesheetAnimation>? animations))
        {
            return animations;
        }

        return new List<SpritesheetAnimation>();
    }

    /// <summary>
    ///     Returns the first occurrence of a <see cref="SpritesheetAnimation"/>
    ///     element with the specified <paramref name="name"/> from this
    ///     <see cref="Spritesheet"/>.
    /// </summary>
    /// <param name="name">
    ///     The name of the <see cref="SpritesheetAnimation"/> element to
    ///     return.
    /// </param>
    /// <returns>
    ///     The first occurrence of a <see cref="SpritesheetAnimation"/> element
    ///     with the specified <paramref name="name"/> from this
    ///     this <see cref="Spritesheet"/>, if one is found; otherwise,
    ///     <see langword="null"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException"></exception>
    public SpritesheetAnimation? GetFirstAnimationWithName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name), $"{nameof(name)} cannot be null or an empty string");
        }

        return GetAnimationsByName(name).FirstOrDefault();
    }
}