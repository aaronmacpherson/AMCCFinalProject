/* Shared.cs
 * Final Project
 * Revision History
 *      Cynthia Cheng: 2016.12.1: Created & Coded
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace AMCCFinalProject
{
    public class Shared
    {
        public static Vector2 stage;
        public static bool gameOver = false;
        public static GraphicsDeviceManager graphics;
        public static GameOverScene gameOverScene;
        public static int currentScore;
        public static int hiScore;
        public static int level = 1;
        public static bool nextLevel = false;
        public static Song bossTheme;
    }
}
