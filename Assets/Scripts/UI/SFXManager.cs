using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [System.Serializable]
    public class Sound
    {
        public AudioClip clip;
        [Range(0f, 3f)] public float volume = 1f;
    }

    [Header("Per-Stage Animal Sounds")]
    public Sound polymerAttack, polymerHurt, polymerDeath;
    public Sound eukaryoteAttack, eukaryoteHurt, eukaryoteDeath;
    public Sound jellyAttack, jellyHurt, jellyDeath;
    public Sound tiktaalikAttack, tiktaalikHurt, tiktaalikDeath;
    public Sound durlsthoAttack, durlsthoHurt, durlsthoDeath;
    public Sound sugargliderAttack, sugargliderHurt, sugargliderDeath;
    public Sound jerboaAttack, jerboaHurt, jerboaDeath;

    [Header("Generic Sounds")]
    public Sound eat;
    public Sound evolutionReady;
    public Sound staminaDepleted;
    public Sound uiClick;
    public Sound sugargliderFlap;
    public Sound jerboaJump;

    [Range(0f, 1f)] public float baseVolume = 1f;
    [HideInInspector] public float optionsMultiplier = 1f;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    string CurrentAnimal()
    {
        string s = SceneManager.GetActiveScene().name;
        if (s == "Stage1") return "polymer";
        if (s == "Stage2") return "eukaryote";
        if (s == "Stage3") return "jelly";
        if (s == "Stage4") return "tiktaalik";
        if (s == "Stage5") return "durlstho";
        if (s == "Stage6_Fly") return "sugarglider";
        if (s == "Stage6_Jump") return "jerboa";
        return "";
    }

    public void PlayAttack()
    {
        Sound s = null;
        switch (CurrentAnimal())
        {
            case "polymer": s = polymerAttack; break;
            case "eukaryote": s = eukaryoteAttack; break;
            case "jelly": s = jellyAttack; break;
            case "tiktaalik": s = tiktaalikAttack; break;
            case "durlstho": s = durlsthoAttack; break;
            case "sugarglider": s = sugargliderAttack; break;
            case "jerboa": s = jerboaAttack; break;
        }
        Play(s);
    }

    public void PlayHurt()
    {
        Sound s = null;
        switch (CurrentAnimal())
        {
            case "polymer": s = polymerHurt; break;
            case "eukaryote": s = eukaryoteHurt; break;
            case "jelly": s = jellyHurt; break;
            case "tiktaalik": s = tiktaalikHurt; break;
            case "durlstho": s = durlsthoHurt; break;
            case "sugarglider": s = sugargliderHurt; break;
            case "jerboa": s = jerboaHurt; break;
        }
        Play(s);
    }

    public void PlayDeath()
    {
        Sound s = null;
        switch (CurrentAnimal())
        {
            case "polymer": s = polymerDeath; break;
            case "eukaryote": s = eukaryoteDeath; break;
            case "jelly": s = jellyDeath; break;
            case "tiktaalik": s = tiktaalikDeath; break;
            case "durlstho": s = durlsthoDeath; break;
            case "sugarglider": s = sugargliderDeath; break;
            case "jerboa": s = jerboaDeath; break;
        }
        Play(s);
    }

    public void PlayEat() { Play(eat); }
    public void PlayEvolutionReady() { Play(evolutionReady); }
    public void PlayStaminaDepleted() { Play(staminaDepleted); }
    public void PlayUIClick() { Play(uiClick); }
    public void PlaySugarGliderFlap() { Play(sugargliderFlap); }
    public void PlayJerboaJump() { Play(jerboaJump); }

    void Play(Sound s)
    {
        if (s != null && s.clip != null)
            audioSource.PlayOneShot(s.clip, s.volume * baseVolume * optionsMultiplier);
    }
}
