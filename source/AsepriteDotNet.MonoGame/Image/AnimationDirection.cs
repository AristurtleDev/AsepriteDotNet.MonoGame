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

/*
    This is an intentional duplication of the LoopDirection enum from
    AsepriteDotNet.Image with the addition of the PingPongReverse value.
    Duplication is made so that the AsepriteDotNet core package remains
    a private asset
*/

/// <summary>
///     Defines the direction in which an animation enumerates.
/// </summary>
public enum AnimationDirection
{
    /// <summary>
    ///     Defines that the animation enumerates in a forward direction
    ///     starting with the first frame and ending with the last frame.
    /// </summary>
    Forward = 0,

    /// <summary>
    ///     Defines that the animation enumerates in a reverse direction
    ///     starting on the last frame and ending on the first frame.
    /// </summary>
    Reverse = 1,

    /// <summary>
    ///     Defines that the animation enumerates in a ping-pong direction 
    ///     starting on the first frame and moving forward to the last frame,
    ///     then moves back in reverse to the first frame.
    /// </summary>
    PingPong = 2,

    /// <summary>
    ///     Defines that the animation enumerates in a reverse ping-pong
    ///     direction starting on the last frame and moving in reverse to the
    ///     first frame, then moving forward to the last frame.
    /// </summary>
    ReversePingPing = 3
}