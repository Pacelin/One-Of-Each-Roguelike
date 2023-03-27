using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHolder : Pickup
{
    public Rigidbody2D Rigidbody;
    [HideInInspector] public Upgrades Upgrades;
    
    [HideInInspector] public GameObject UpgradesInfo;
    [HideInInspector] public TMP_Text UpgradeNameText;
    [HideInInspector] public TMP_Text UpgradeDescriptionText;
    [HideInInspector] public TMP_Text UpgradeQualityText;
    [HideInInspector] public ItemQualityColors QualityColors;

    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _collectableMaterial;
    [SerializeField] private SpriteRenderer _upgradeSpriteRenderer;

    private Upgrade _holdedUpgrade;

    private void Awake()
    {
        _upgradeSpriteRenderer.material = _defaultMaterial;
    }

    public void SetUpgrade(Upgrade upgrade)
    {
        _holdedUpgrade = upgrade;
        _upgradeSpriteRenderer.sprite = upgrade.Sprite;
    }

    public override void OnCollect()
    {
        Upgrades.AddUpgrade(_holdedUpgrade);
        UpgradesInfo.SetActive(false);
        Destroy(gameObject);
    }

    public override void OnCollectEnter()
    {
        _upgradeSpriteRenderer.material = _collectableMaterial;
        UpgradesInfo.SetActive(true);
        ShowInfo();
    }

    public override void OnCollectExit()
    {
        _upgradeSpriteRenderer.material = _defaultMaterial;
        UpgradesInfo.SetActive(false);
    }
    
    private void ShowInfo()
    {
        UpgradeNameText.text = _holdedUpgrade.Name;
        UpgradeDescriptionText.text = _holdedUpgrade.Description;
        UpgradeQualityText.text = QualityColors.GetName(_holdedUpgrade.Quality);
        UpgradeQualityText.color = QualityColors.GetColor(_holdedUpgrade.Quality);
        UpgradesInfo.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform) UpgradesInfo.transform);
    }
}