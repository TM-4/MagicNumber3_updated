using System;
using System.Media;
using System.Windows.Forms;
using static MagicNumber3.ResourceText;

namespace MagicNumber3
{
    public partial class PointOutForm : Form
    {
        //MainFormを生成(SoundPlayメソッドを使用したいため)
        MainForm mainForm = new MainForm();

        //Loggerクラスを生成
        Logger logger = new Logger();

        public PointOutForm()
        {
            InitializeComponent();

            //「どれが答えでしょう」音声
            mainForm.SoundPlay(soundPath7);
        }

        /// <summary>
        /// 不正解のボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPointOutSelect1Clicked(object sender, EventArgs e)
        {
            //不正解時は「違いますよ！」と音声＆表示
            mainForm.SoundPlay(soundPath8);
            MessageBox.Show(stringPointOut1);
        }

        /// <summary>
        /// 正解のボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPointOutSelect2Clicked(object sender, EventArgs e)
        {
            //正解をクリックするとアプリを閉じる
            //「正解です」音声＆表示
            mainForm.SoundPlay(soundPath9);
            MessageBox.Show(stringPointOut2);

            //「最初の数字で割るとどんな数字でも必ず10になりますね！」音声＆表示
            mainForm.SoundPlay(soundPath10);
            MessageBox.Show(stringPointOut3);

            //「お疲れ様でした！」音声＆表示
            mainForm.SoundPlay(soundPath11);
            MessageBox.Show(stringPointOut4);

            //ログに【詳細】と「プロセスが正常に完了しました。」と表示
            logger.Savelog(Loglevels.INFO, logInfoNormalEnd);
            Close();
        }

        /// <summary>
        /// 不正解のボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPointOutSelect3Clicked(object sender, EventArgs e)
        {
            //不正解時は「違いますよ！」と音声＆表示
            mainForm.SoundPlay(soundPath8);
            MessageBox.Show(stringPointOut1);
        }
    }
}
