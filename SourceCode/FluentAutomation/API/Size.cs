using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.API
{
    /// <summary>
    /// Stores Width, Height values for an Element
    /// </summary>
    public class Size
    {
        private int _width = 0;
        private int _height = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Size"/> class.
        /// </summary>
        public Size()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Size(int width, int height)
        {
            _width = width;
            _height = height;
        }

        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
    }
}
