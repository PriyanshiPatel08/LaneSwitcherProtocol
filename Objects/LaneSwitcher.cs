using System;

namespace LaneSwitcherProtocol.Objects
{
    // Minimal implementation of LaneSwitcher expected by Game1.
    public class LaneSwitcher
    {
        private int currentLane;
        private int laneWidth;
        private double cooldownSeconds;
        private double cooldownRemaining;

        public LaneSwitcher(int startLane = 1, int laneWidth = 150, double cooldownSeconds = 0.25)
        {
            this.currentLane = startLane;
            this.laneWidth = laneWidth;
            this.cooldownSeconds = cooldownSeconds;
            this.cooldownRemaining = 0.0;
        }

        public void MoveLeft()
        {
            if (cooldownRemaining > 0) return;
            currentLane--;
            if (currentLane < 0) currentLane = 0;
            cooldownRemaining = cooldownSeconds;
        }

        public void MoveRight()
        {
            if (cooldownRemaining > 0) return;
            currentLane++;
            cooldownRemaining = cooldownSeconds;
        }

        public void Update(double elapsedSeconds)
        {
            if (cooldownRemaining <= 0) return;
            cooldownRemaining -= elapsedSeconds;
            if (cooldownRemaining < 0) cooldownRemaining = 0;
        }

        // Returns the X position for drawing given the screen center X.
        public int GetXPosition(int centerX)
        {
            // Treat startLane == 1 as centered; compute offset in pixels
            int offset = (currentLane - 1) * laneWidth;
            return centerX + offset;
        }
    }
}
