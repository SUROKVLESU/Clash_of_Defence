
public class WaveController
{
    public WaveController()
    {

    }
    public void ResetBuildingsAndEnemies()
    {
        GameController.instance.MapController.ActivationBuildings();
        GameController.instance.EnemiesController.DestroyEnemies();
    }
    public void Defeat()
    {
        GameController.instance.DefeatInterfece.SetActive(true);
        GameController.instance.InterfeceHand.SetActive(false);
    }
}

