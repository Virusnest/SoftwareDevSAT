using System.Drawing;
using System.Numerics;
using Foster.Framework;
using Color = Foster.Framework.Color;

namespace KnuckleBonesGame.Util;

public static class BatcherExtentions {
  public static void DrawNineSlice(
        this Batcher batcher,
        Texture tex,
        RectInt srcCenter,
        RectInt dest,
        Color? color = null,
        int layer = 0)
    {
        // ── border thickness derived from srcCenter ──────────────
        int left   = srcCenter.X;
        int top    = srcCenter.Y;
        int right  = tex.Width  - srcCenter.Right;
        int bottom = tex.Height - srcCenter.Bottom;

        if (dest.Width < left + right || dest.Height < top + bottom)
            throw new ArgumentException("Destination smaller than fixed borders.");

        Color tint = color ?? Color.White;

        // source rectangles
        var srcTL = new RectInt(0,               0,              left,   top);
        var srcTR = new RectInt(tex.Width-right, 0,              right,  top);
        var srcBL = new RectInt(0,               tex.Height-bottom, left,   bottom);
        var srcBR = new RectInt(tex.Width-right, tex.Height-bottom, right,  bottom);

        var srcH  = new RectInt(left, 0,                       srcCenter.Width,  top);
        var srcV  = new RectInt(0,    top,                     left,            srcCenter.Height);
        var srcC  = srcCenter;
        // local helper
        void Tile(RectInt src, RectInt dst) =>
            batcher.Image(tex,
                src,
                new Vector2(dst.X, dst.Y),
                Vector2.Zero,
              new Vector2((float)dst.Width  / src.Width,
                  (float)dst.Height / src.Height),
              0,
                tint
                          );

        // ── corners ───────────────────────────────────────────────
        Tile(srcTL, new(dest.X,               dest.Y,                left,   top));
        Tile(srcTR, new(dest.Right - right,   dest.Y,                right,  top));
        Tile(srcBL, new(dest.X,               dest.Bottom - bottom,  left,   bottom));
        Tile(srcBR, new(dest.Right - right,   dest.Bottom - bottom,  right,  bottom));

        // ── horizontal edges ──────────────────────────────────────
        int innerW = dest.Width - left - right;
        for (int x = 0; x < innerW; x += srcH.Width)
        {
            int w = System.Math.Min(srcH.Width, innerW - x);
            var slice = new RectInt(srcH.X, srcH.Y, w, srcH.Height);

            Tile(slice, new(dest.X + left + x, dest.Y,                 w, top));
            Tile(slice, new(dest.X + left + x, dest.Bottom - bottom,   w, bottom));
        }

        // ── vertical edges ────────────────────────────────────────
        int innerH = dest.Height - top - bottom;
        for (int y = 0; y < innerH; y += srcV.Height)
        {
            int h = System.Math.Min(srcV.Height, innerH - y);
            var slice = new RectInt(srcV.X, srcV.Y, srcV.Width, h);

            Tile(slice, new(dest.X,               dest.Y + top + y, left,  h));
            Tile(slice, new(dest.Right - right,   dest.Y + top + y, right, h));
        }

        // ── centre tiles ──────────────────────────────────────────
        for (int y = 0; y < innerH; y += srcC.Height)
        {
            int h = System.Math.Min(srcC.Height, innerH - y);
            for (int x = 0; x < innerW; x += srcC.Width)
            {
                int w = System.Math.Min(srcC.Width, innerW - x);
                var slice = new RectInt(srcC.X, srcC.Y, w, h);

                Tile(slice, new(dest.X + left + x,
                                dest.Y + top  + y,
                                w, h));
            }
        }
    }

}