using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine;
using Newtonsoft.Json;

public class TableManager 
{
    public TableManager()
    {
    }
    public GlobalConfig GlobalConfig { get; private set; }
    public AudioConfig AudioConfig { get; private set; }

    #region Sheet
    public CharacterConfig_SO CharacterConfig{ get; private set; }
    #endregion

    public async Task Init()
    {
        GlobalConfig=await Addressables.LoadAssetAsync<GlobalConfig>(typeof(GlobalConfig).ToString()).Task;
        AudioConfig=await Addressables.LoadAssetAsync<AudioConfig>(typeof(AudioConfig).ToString()).Task;
        AudioConfig.Init();


        CharacterConfig = await Addressables.LoadAssetAsync<CharacterConfig_SO>(typeof(CharacterConfig).ToString()).Task;
        CharacterConfig.Init();
    }

}
