/*
    Author: Lucy Malmud
    Date Created: 10/30/2025
    Date Last Updated: 10/30/2025
    Summary: Will allow enemies to take effects.
*/

public interface IApplyEffect
{
    // called by enemy when a bullet hits it
    void ApplyTo(Enemy enemy);
}