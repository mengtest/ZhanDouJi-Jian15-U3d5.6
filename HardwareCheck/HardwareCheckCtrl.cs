using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class HardwareCheckCtrl : MonoBehaviour {

	public UILabel[] BiZhiLable;
	public UILabel[] QiangPosLable;
	public UILabel[] QiangZDLable;
	public UILabel AnJianLable;
	public UILabel StartLedP1;
	public UILabel StartLedP2;
	public UILabel[] QiNangLabel;
	public GameObject JiaMiCeShiObj;
	public bool IsJiaMiCeShi;
	int StartLedNumP1 = 1;
	int StartLedNumP2 = 1;
	public static bool IsTestHardWare;
	public static HardwareCheckCtrl Instance;

	// Use this for initialization
	void Start()
	{
		Screen.SetResolution(1360, 768, false);
		Instance = this;
		IsTestHardWare = true;
		JiaMiCeShiObj.SetActive(IsJiaMiCeShi);
		BiZhiLable[0].text = "0";
		BiZhiLable[1].text = "0";
		AnJianLable.text = "...";

		HardwareBtCtrl.StartLedP1 = StartLedP1;
		HardwareBtCtrl.StartLedP2 = StartLedP2;

		InputEventCtrl.GetInstance().ClickSetEnterBtEvent += ClickSetEnterBtEvent;
		InputEventCtrl.GetInstance().ClickSetMoveBtEvent += ClickSetMoveBtEvent;
		InputEventCtrl.GetInstance().ClickFireBtOneEvent += ClickFireBtOneEvent;
		InputEventCtrl.GetInstance().ClickFireBtTwoEvent += ClickFireBtTwoEvent;
		InputEventCtrl.GetInstance().ClickDaoDanBtOneEvent += ClickDaoDanBtOneEvent;
		InputEventCtrl.GetInstance().ClickDaoDanBtTwoEvent += ClickDaoDanBtTwoEvent;
		InputEventCtrl.GetInstance().ClickStartBtOneEvent += ClickStartP1BtEvent;
		InputEventCtrl.GetInstance().ClickStartBtTwoEvent += ClickStartP2BtEvent;
		InputEventCtrl.GetInstance().ClickStopDongGanBtOneEvent += ClickStopDongGanBtOneEvent;
	}
	
	// Update is called once per frame
	void Update()
	{
		BiZhiLable[0].text = XKGlobalData.CoinPlayerOne.ToString();
		BiZhiLable[1].text = XKGlobalData.CoinPlayerTwo.ToString();
		QiangPosLable[0].text = "X-> "+pcvr.MousePositionP1.x.ToString();
		QiangPosLable[1].text = "Y-> "+pcvr.MousePositionP1.y.ToString();
		QiangPosLable[2].text = "X-> "+pcvr.MousePositionP2.x.ToString();
		QiangPosLable[3].text = "Y-> "+pcvr.MousePositionP2.y.ToString();
	}

	void ClickSetEnterBtEvent(ButtonState val)
	{
		if (val == ButtonState.DOWN) {
			AnJianLable.text = "设置 Down";
		}
		else {
			AnJianLable.text = "设置 Up";
		}
	}

	void ClickSetMoveBtEvent(ButtonState val)
	{
		if (val == ButtonState.DOWN) {
			AnJianLable.text = "移动 Down";
		}
		else {
			AnJianLable.text = "移动 Up";
		}
	}

	void ClickStartP1BtEvent(ButtonState val)
	{
		if (val == ButtonState.DOWN) {
			AnJianLable.text = "1P开始 Down";
		}
		else {
			AnJianLable.text = "1P开始 Up";
		}
	}

	void ClickStartP2BtEvent(ButtonState val)
	{
		if (val == ButtonState.DOWN) {
			AnJianLable.text = "2P开始 Down";
		}
		else {
			AnJianLable.text = "2P开始 Up";
		}
	}
	
	void ClickStopDongGanBtOneEvent(ButtonState val)
	{
		if (val == ButtonState.DOWN) {
			AnJianLable.text = "紧急 Down";
		}
		else {
			AnJianLable.text = "紧急 Up";
		}
	}

	void ClickFireBtOneEvent(ButtonState val)
	{
		if (val == ButtonState.DOWN) {
			AnJianLable.text = "1P射击 Down";
		}
		else {
			AnJianLable.text = "1P射击 Up";
		}
	}

	void ClickFireBtTwoEvent(ButtonState val)
	{
		if (val == ButtonState.DOWN) {
			AnJianLable.text = "2P射击 Down";
		}
		else {
			AnJianLable.text = "2P射击 Up";
		}
	}
	
	void ClickDaoDanBtOneEvent(ButtonState val)
	{
		if (val == ButtonState.DOWN) {
			AnJianLable.text = "1P导弹 Down";
		}
		else {
			AnJianLable.text = "1P导弹 Up";
		}
	}
	
	void ClickDaoDanBtTwoEvent(ButtonState val)
	{
		if (val == ButtonState.DOWN) {
			AnJianLable.text = "2P导弹 Down";
		}
		else {
			AnJianLable.text = "2P导弹 Up";
		}
	}

	public UILabel JiaMiJYLabel;
	public UILabel JiaMiJYMsg;
	public static bool IsOpenJiaMiJiaoYan;
	void CloseJiaMiJiaoYanFailed()
	{
		if (!IsInvoking("JiaMiJiaoYanFailed")) {
			return;
		}
		CancelInvoke("JiaMiJiaoYanFailed");
	}

	public void JiaMiJiaoYanFailed()
	{
		SetJiaMiJYMsg("", JiaMiJiaoYanEnum.Failed);
	}
	
	public void JiaMiJiaoYanSucceed()
	{
		SetJiaMiJYMsg("", JiaMiJiaoYanEnum.Succeed);
	}

	public void SetJiaMiJYMsg(string msg, JiaMiJiaoYanEnum key)
	{
		switch (key) {
		case JiaMiJiaoYanEnum.Succeed:
			CloseJiaMiJiaoYanFailed();
			JiaMiJYMsg.text = "校验成功";
			ResetJiaMiJYLabelInfo();
			ScreenLog.Log("校验成功");
			break;
			
		case JiaMiJiaoYanEnum.Failed:
			CloseJiaMiJiaoYanFailed();
			JiaMiJYMsg.text = "校验失败";
			ResetJiaMiJYLabelInfo();
			ScreenLog.Log("校验失败");
			break;
			
		default:
			JiaMiJYMsg.text = msg;
			ScreenLog.Log(msg);
			break;
		}
	}
	
	public static void CloseJiaMiJiaoYan()
	{
		if (!IsOpenJiaMiJiaoYan) {
			return;
		}
		IsOpenJiaMiJiaoYan = false;
	}

	void ResetJiaMiJYLabelInfo()
	{
		CloseJiaMiJiaoYan();
		JiaMiJYLabel.text = "加密校验";
	}

	public void SubCoinPOne()
	{
		if (XKGlobalData.CoinPlayerOne < 1) {
			return;
		}
		XKGlobalData.CoinPlayerOne--;
		pcvr.GetInstance().SubPlayerCoin(1, PlayerEnum.PlayerOne);
	}
	
	public void SubCoinPTwo()
	{
		if (XKGlobalData.CoinPlayerTwo < 1) {
			return;
		}
		XKGlobalData.CoinPlayerTwo--;
		pcvr.GetInstance().SubPlayerCoin(1, PlayerEnum.PlayerTwo);
	}

	public void StartLedCheckP1()
	{
		StartLedNumP1++;
		switch (StartLedNumP1) {
		case 1:
			StartLedP1.text = "1P开始灯亮";
			pcvr.StartLightStateP1 = LedState.Liang;
			break;
			
		case 2:
			StartLedP1.text = "1P开始灯闪";
			pcvr.StartLightStateP1 = LedState.Shan;
			break;
			
		case 3:
			StartLedP1.text = "1P开始灯灭";
			pcvr.StartLightStateP1 = LedState.Mie;
			StartLedNumP1 = 1;
			break;
		}
	}
	
	public void StartLedCheckP2()
	{
		StartLedNumP2++;
		switch (StartLedNumP2) {
		case 1:
			StartLedP2.text = "2P开始灯亮";
			pcvr.StartLightStateP2 = LedState.Liang;
			break;
			
		case 2:
			StartLedP2.text = "2P开始灯闪";
			pcvr.StartLightStateP2 = LedState.Shan;
			break;
			
		case 3:
			StartLedP2.text = "2P开始灯灭";
			pcvr.StartLightStateP2 = LedState.Mie;
			StartLedNumP2 = 1;
			break;
		}
	}

	public void OnClickCloseAppBt()
	{
		Application.Quit();
	}
	
	public void OnClickRestartAppBt()
	{
		Application.Quit();
		XKCheckGameServerIP.RunCmd("start ComTest.exe");
	}

	public void StartJiaoYanIO()
	{
		pcvr.GetInstance().StartJiaoYanIO();
	}

	public void OnClickQiNangBt_1()
	{
		QiNangLabel[0].text = QiNangLabel[0].text != "1气囊充气" ? "1气囊充气" : "1气囊放气";
		pcvr.QiNangArray[0] = (byte)(pcvr.QiNangArray[0] != 1 ? 1 : 0);
	}

	public void OnClickQiNangBt_2()
	{
		QiNangLabel[1].text = QiNangLabel[1].text != "2气囊充气" ? "2气囊充气" : "2气囊放气";
		pcvr.QiNangArray[1] = (byte)(pcvr.QiNangArray[1] != 1 ? 1 : 0);
	}

	public void OnClickQiNangBt_3()
	{
		QiNangLabel[2].text = QiNangLabel[2].text != "3气囊充气" ? "3气囊充气" : "3气囊放气";
		pcvr.QiNangArray[2] = (byte)(pcvr.QiNangArray[2] != 1 ? 1 : 0);
	}

	public void OnClickQiNangBt_4()
	{
		QiNangLabel[3].text = QiNangLabel[3].text != "4气囊充气" ? "4气囊充气" : "4气囊放气";
		pcvr.QiNangArray[3] = (byte)(pcvr.QiNangArray[3] != 1 ? 1 : 0);
	}

	public void SetQiangZDValue_1()
	{
		if (UIProgressBar.current == null) {
			return;
		}
		int valZD = Mathf.RoundToInt(UIProgressBar.current.value * 15f);
		QiangZDLable[0].text = valZD.ToString("X2");
		pcvr.QiangZhenDongP1 = valZD;
	}

	public void SetQiangZDValue_2()
	{
		if (UIProgressBar.current == null) {
			return;
		}
		int valZD = Mathf.RoundToInt(UIProgressBar.current.value * 15f);
		QiangZDLable[1].text = valZD.ToString("X2");
		pcvr.QiangZhenDongP2 = valZD;
	}
}

public enum JiaMiJiaoYanEnum
{
	Null,
	Succeed,
	Failed,
}