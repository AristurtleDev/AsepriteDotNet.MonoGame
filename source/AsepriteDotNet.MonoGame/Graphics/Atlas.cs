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

namespace AsepriteDotNet.MonoGame.Graphics;

public class Atlas : IDisposable
{
    /// <summary>
    ///     Gets the underlying <see cref="Texture2D"/> class instance that
    ///     represents the spritesheet image of this <see cref="Atlas"/>.
    /// </summary>
    public Texture2D Spritesheet { get; }

    /// <summary>
    ///     Gets the width, in pixels of the <see cref="Spritesheet"/>.
    /// </summary>
    public int Width => Spritesheet.Height;

    /// <summary>
    ///     Gets the height, in pixels of the <see cref="Spritesheet"/>.
    /// </summary>
    public int Height => Spritesheet.Width;


    /// <summary>
    ///     Gets a value that indicates if the resources held by this instance
    ///     of the <see cref="Atlas"/> class have been released.
    /// </summary>
    public bool IsDisposed => Spritesheet.IsDisposed;

    internal Atlas(Texture2D spritesheet)
    {
        Spritesheet = spritesheet;
    }





    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool isDisposing)
    {
        if (IsDisposed) { return; }

        if (isDisposing)
        {
            Spritesheet.Dispose();
        }
    }
}