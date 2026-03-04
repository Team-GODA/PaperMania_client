using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GraphicsManager : SingleMono<GraphicsManager>
{
	public GraphicsType graphicsType = GraphicsType.Ultra;
	public HzType hzType = HzType.HighHz;

	[SerializeField] private List<RenderPipelineAsset> renderPipelineAssets;

	private void Start()
	{
		if (PlayerPrefs.HasKey("graphicsType")) graphicsType = (GraphicsType)PlayerPrefs.GetInt("graphicsType");
		if (PlayerPrefs.HasKey("hzType")) hzType = (HzType)PlayerPrefs.GetInt("hzType");

		onUpdateHz();
		onUpdateGraphics();
	}

	public void UpdateHz(int type)
	{
		hzType = (HzType)type;
		onUpdateHz();
	}
	
	public void UpdateHz(HzType type)
	{
		hzType = type;
		onUpdateHz();
	}

	public void UpdateGraphics(GraphicsType type)
	{
		graphicsType = type;
		onUpdateGraphics();
	}

	public void UpdateGraphics(int type)
	{
		graphicsType = (GraphicsType)type;
		onUpdateGraphics();
	}

	private void onUpdateHz()
	{
		PlayerPrefs.SetInt("hzType", (int)hzType);
		Application.targetFrameRate = 30 * (int)hzType;
	}

	private void onUpdateGraphics()
	{
		PlayerPrefs.SetInt("graphicsType", (int)graphicsType);
		QualitySettings.SetQualityLevel((int)graphicsType);
		QualitySettings.renderPipeline = renderPipelineAssets[(int)graphicsType];
	}
}

public enum GraphicsType
{
	Low,
	Medium,
	High,
	Ultra
}

public enum HzType
{
	LowHz = 1,
	HighHz
}