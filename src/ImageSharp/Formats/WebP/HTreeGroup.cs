// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace SixLabors.ImageSharp.Formats.WebP
{
    /// <summary>
    /// Huffman table group.
    /// Includes special handling for the following cases:
    ///  - is_trivial_literal: one common literal base for RED/BLUE/ALPHA (not GREEN)
    ///  - is_trivial_code: only 1 code (no bit is read from bitstream)
    ///  - use_packed_table: few enough literal symbols, so all the bit codes
    ///    can fit into a small look-up table packed_table[]
    /// The common literal base, if applicable, is stored in 'literal_arb'.
    /// </summary>
    internal class HTreeGroup
    {
        public HTreeGroup(uint packedTableSize)
        {
            this.HTrees = new List<HuffmanCode[]>(WebPConstants.HuffmanCodesPerMetaCode);
            this.PackedTable = new HuffmanCode[packedTableSize];
            for (int i = 0; i < packedTableSize; i++)
            {
                this.PackedTable[i] = new HuffmanCode();
            }
        }

        /// <summary>
        /// This has a maximum of HuffmanCodesPerMetaCode (5) entry's.
        /// </summary>
        public List<HuffmanCode[]> HTrees { get; private set; }

        /// <summary>
        /// True, if huffman trees for Red, Blue & Alpha Symbols are trivial (have a single code).
        /// </summary>
        public bool IsTrivialLiteral { get; set; }

        /// <summary>
        /// If is_trivial_literal is true, this is the ARGB value of the pixel, with Green channel being set to zero.
        /// </summary>
        public uint LiteralArb { get; set; }

        /// <summary>
        /// True if is_trivial_literal with only one code.
        /// </summary>
        public bool IsTrivialCode { get; set; }

        /// <summary>
        /// use packed table below for short literal code
        /// </summary>
        public bool UsePackedTable { get; set; }

        /// <summary>
        /// Table mapping input bits to a packed values, or escape case to literal code.
        /// </summary>
        public HuffmanCode[] PackedTable { get; set; }
    }
}
