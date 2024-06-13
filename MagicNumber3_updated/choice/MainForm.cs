using System;
using System.Media;
using System.Windows.Forms;
using static MagicNumber3.ResourceText;

namespace MagicNumber3
{
    public partial class MainForm : Form
    {
        #region フィールド
        /// <summary>
        /// フィールド
        /// </summary>
        // 次に入力するべき数字を格納する変数
        int nextNum = 0;

        //switch用カウント変数
        int clickCount = 0;

        //Loggerクラスを生成
        Logger logger = new Logger();
        #endregion

        #region コンストラクタ
        public MainForm()
        {
            InitializeComponent();

            //初期選択を決定ボタンに合わせる
            AcceptButton = btnDecision;
        }
        #endregion

        #region イベント
        /// <summary>
        /// メインフォームロードイベント
        /// カーソルをテキストボックスにあわせる
        /// </summary>
        #region MainForm_Load
        private void MainForm_Load(object sender, EventArgs e)
        {
            //List<string> soundPathes = new List<string>()
            //{
            //    "VOICEVOX_SoundData\\どんな数字も最後には３になってしまいます。...怪しいポイントでトリックを見破ってみましょう！.wav"
            //};

            //ルール説明の文言を音声ファイルで再生
            SoundPlay(soundPath0);

            //ルール説明の文言を表示
            MessageBox.Show(stringOp1 + Environment.NewLine + stringOp2);

            // カーソルをテキストボックスにあわせる
            ActiveControl = enterNum;
        }
        #endregion

        /// <summary>
        /// 決定ボタンを押したときの処理
        /// </summary>
        #region BtnDecisionClicked
        public void BtnDecisionClicked(object sender, EventArgs e)
        {
            clickCount++;

            switch (clickCount)
            {
                case 1:
                    //最初に入力された数字をfirstNumラベルに表示
                    firstNum.Text = enterNum.Text;

                    //switch毎の毎回の処理
                    //「その数字に5を掛けた数字を入力してください」音声＆表示
                    SwitchEvent(soundPath1, stringBtnDecisionClicked1);

                    //次に入力するべき数字を計算して格納
                    nextNum *= 5;
                    return;

                case 2:
                    if (nextNum == Int32.Parse(enterNum.Text))
                    {
                        //switch毎の毎回の処理
                        //次の指示「その数字に2を掛けた数字を入力してください」音声＆表示
                        SwitchEvent(soundPath2, stringBtnDecisionClicked2);

                        //次に入力するべき数字を計算して格納
                        nextNum *= 2;
                        return;
                    }
                    break;

                case 3:
                    if (nextNum == Int32.Parse(enterNum.Text))
                    {
                        //switch毎の毎回の処理
                        //次の指示「最初の数字で割ってください」音声＆表示
                        SwitchEvent(soundPath3, stringBtnDecisionClicked3);

                        //次に入力するべき数字を計算して格納
                        nextNum = nextNum / Int32.Parse(firstNum.Text);
                        return;
                    }
                    break;

                case 4:
                    if (nextNum == Int32.Parse(enterNum.Text))
                    {
                        //「７を引いてください。」音声＆表示
                        SoundPlay(soundPath4);
                        MessageBox.Show(stringBtnDecisionClicked4);

                        //「３になりましたね？」音声＆表示
                        SoundPlay(soundPath5);
                        MessageBox.Show(stringBtnDecisionClicked5);

                        //ログに【詳細】と「プロセスが正常に完了しました。」と表示
                        logger.Savelog(Loglevels.INFO, logInfoNormalEnd);

                        //アプリを終了する
                        Close();
                        return;
                    }
                    break;
            }

            if (clickCount != 1)
            {
                //ユーザーが入力をミスした際の処理
                MissNumber();
            }
        }
        #endregion

        /// <summary>
        /// 入力ミスの際の処理
        /// </summary>
        #region MissNumber
        private void MissNumber()
        {
            //正しい数値を入力してください。と表示する
            MessageBox.Show(stringMissNumber1);
            //Switchで使用するクリックカウントを減らす
            clickCount--;
            //ログに【エラー】と「正しくない数字が入力されました。」と表示
            logger.Savelog(Loglevels.ERROR, logErrorIncorrectNum);
            //enterNumが選択されている状態に戻す
            ActiveControl = enterNum;
        }
        #endregion

        /// <summary>
        /// switch毎の毎回の処理
        /// </summary>
        #region SwitchEvent
        private void SwitchEvent(string filePath,string stringBtnDecisionClicked)
        {
            //音声ファイルのパスを渡して次の指示を音声で再生
            SoundPlay(filePath);

            //次の指示を表示
            text.Text = stringBtnDecisionClicked;

            //入力された数値をlblEnterNumに表示する
            nextNum = Int32.Parse(enterNum.Text);
            lblEnterNum.Text = nextNum.ToString();

            //enterNumを空欄にする
            enterNum.Text = stringSwitchEvent;

            //enterNumが選択されている状態に戻す
            ActiveControl = enterNum;
        }
        #endregion

        /// <summary>
        /// case 3:で表示できるようにするトリックを指摘するボタンの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region BtnPointOutClicked
        private void BtnPointOutClicked(object sender, EventArgs e)
        {
            if (clickCount == 3)
            {
                ///PointOutForm を表示
                PointOutForm pointOutForm = new PointOutForm();
                pointOutForm.ShowDialog();

                //アプリを終了する
                Close();
            }
            else
            {
                //「ここではありません！」と音声＆表示
                SoundPlay(soundPath6);
                MessageBox.Show(stringMissNumber2);
            }
        }
        #endregion

        /// <summary>
        /// 音声を再生する
        /// </summary>
        /// <param name="filePath">音声データのファイルパス</param>
        #region SoundPlay
        public void SoundPlay(string filePath)
        {
            //音声を再生する
            SoundPlayer player = new SoundPlayer(filePath);
            player.Play();
        }
        #endregion
        #endregion
    }
}
