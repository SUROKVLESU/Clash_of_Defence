
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
    public void EnemiesDefeat()
    {
        GameController.instance.MapController.GetResources();
        GameController.instance.WaveController.ResetBuildingsAndEnemies();
        GameController.instance.ButtonController.OnInterfeceHand();
    }
    public void PlayerDefeat()
    {
        GameController.instance.EnemiesController.DestroyEnemies();
        GameController.instance.WaveController.Defeat();
    }
}

