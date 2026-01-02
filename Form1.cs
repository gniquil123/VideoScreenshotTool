using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VideoScreenshotTool
{
    public partial class Form1 : Form
    {
        // 获取应用程序所在目录
        private readonly string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        // 配置和日志文件都放在应用程序所在目录
        private readonly string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt");
        private readonly string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
        
        // 主题枚举
        private enum ThemeType
        {
            Light,
            Dark
        }
        
        // 当前主题
        private ThemeType currentTheme = ThemeType.Dark;

        public Form1()
        {
            InitializeComponent();
            LoadConfig();
            // 确保日志目录存在
            string logDir = Path.GetDirectoryName(logFilePath);
            if (!string.IsNullOrEmpty(logDir))
            {
                Directory.CreateDirectory(logDir);
            }
            // 写入初始化日志
            WriteLog("程序启动，日志系统初始化完成");
            WriteLog($"日志文件路径: {logFilePath}");
        }

        /// <summary>
        /// 写入日志信息到日志文件
        /// </summary>
        /// <param name="message">日志消息</param>
        private void WriteLog(string message)
        {
            string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {message}{Environment.NewLine}";
            try
            {
                // 使用AppendAllText确保线程安全并自动处理文件锁定
                File.AppendAllText(logFilePath, logEntry);
            }
            catch (Exception ex)
            {
                // 日志写入失败时，输出到控制台
                Console.WriteLine($"日志写入失败: {ex.Message}");
                Console.WriteLine($"尝试记录的日志: {logEntry}");
            }
        }

        private void InitializeComponent()
        {
            btnBrowseVideo = new Button();
            txtVideoPath = new TextBox();
            label1 = new Label();
            label2 = new Label();
            numInterval = new NumericUpDown();
            label3 = new Label();
            btnBrowseOutput = new Button();
            txtOutputPath = new TextBox();
            progressBar1 = new ProgressBar();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            txtStartTime = new TextBox();
            txtEndTime = new TextBox();
            btnRenameScreenshots = new Button();
            label7 = new Label();
            label8 = new Label();
            btnRemoveSegment = new Button();
            btnFpsScreenshot = new Button();
            label9 = new Label();
            cmbTheme = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)numInterval).BeginInit();
            SuspendLayout();
            // 
            // btnBrowseVideo
            // 
            btnBrowseVideo.Location = new Point(739, 54);
            btnBrowseVideo.Margin = new Padding(5);
            btnBrowseVideo.Name = "btnBrowseVideo";
            btnBrowseVideo.Size = new Size(118, 37);
            btnBrowseVideo.TabIndex = 0;
            btnBrowseVideo.Text = "浏览...";
            btnBrowseVideo.UseVisualStyleBackColor = true;
            btnBrowseVideo.Click += btnBrowseVideo_Click;
            // 
            // txtVideoPath
            // 
            txtVideoPath.Location = new Point(19, 61);
            txtVideoPath.Margin = new Padding(5);
            txtVideoPath.Name = "txtVideoPath";
            txtVideoPath.Size = new Size(712, 30);
            txtVideoPath.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 24);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(86, 24);
            label1.TabIndex = 2;
            label1.Text = "视频文件:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 117);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(86, 24);
            label2.TabIndex = 3;
            label2.Text = "时间间隔:";
            // 
            // numInterval
            // 
            numInterval.Location = new Point(114, 117);
            numInterval.Margin = new Padding(5);
            numInterval.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numInterval.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numInterval.Name = "numInterval";
            numInterval.Size = new Size(116, 30);
            numInterval.TabIndex = 4;
            numInterval.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(251, 117);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(28, 24);
            label3.TabIndex = 5;
            label3.Text = "秒";
            // 
            // btnBrowseOutput
            // 
            btnBrowseOutput.Location = new Point(741, 196);
            btnBrowseOutput.Margin = new Padding(5);
            btnBrowseOutput.Name = "btnBrowseOutput";
            btnBrowseOutput.Size = new Size(118, 37);
            btnBrowseOutput.TabIndex = 6;
            btnBrowseOutput.Text = "浏览...";
            btnBrowseOutput.UseVisualStyleBackColor = true;
            btnBrowseOutput.Click += btnBrowseOutput_Click;
            // 
            // txtOutputPath
            // 
            txtOutputPath.Location = new Point(19, 203);
            txtOutputPath.Margin = new Padding(5);
            txtOutputPath.Name = "txtOutputPath";
            txtOutputPath.Size = new Size(712, 30);
            txtOutputPath.TabIndex = 7;
            // 
            // progressBar1
            // 
            progressBar1.BackColor = Color.FromArgb(128, 255, 255);
            progressBar1.Location = new Point(19, 484);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(840, 42);
            progressBar1.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(19, 166);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(86, 24);
            label4.TabIndex = 10;
            label4.Text = "输出路径:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(19, 343);
            label5.Margin = new Padding(5, 0, 5, 0);
            label5.Name = "label5";
            label5.Size = new Size(86, 24);
            label5.TabIndex = 11;
            label5.Text = "开始时间:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(241, 343);
            label6.Margin = new Padding(5, 0, 5, 0);
            label6.Name = "label6";
            label6.Size = new Size(86, 24);
            label6.TabIndex = 12;
            label6.Text = "结束时间:";
            // 
            // txtStartTime
            // 
            txtStartTime.Location = new Point(19, 380);
            txtStartTime.Margin = new Padding(5);
            txtStartTime.Name = "txtStartTime";
            txtStartTime.Size = new Size(156, 30);
            txtStartTime.TabIndex = 13;
            txtStartTime.Text = "00:00:00";
            // 
            // txtEndTime
            // 
            txtEndTime.Location = new Point(241, 380);
            txtEndTime.Margin = new Padding(5);
            txtEndTime.Name = "txtEndTime";
            txtEndTime.Size = new Size(169, 30);
            txtEndTime.TabIndex = 14;
            txtEndTime.Text = "00:00:10";
            // 
            // btnRenameScreenshots
            // 
            btnRenameScreenshots.Location = new Point(451, 253);
            btnRenameScreenshots.Margin = new Padding(5);
            btnRenameScreenshots.Name = "btnRenameScreenshots";
            btnRenameScreenshots.Size = new Size(406, 61);
            btnRenameScreenshots.TabIndex = 16;
            btnRenameScreenshots.Text = "直接重命名截图文件\r\n（当只截图而没有重命名时使用）";
            btnRenameScreenshots.UseVisualStyleBackColor = true;
            btnRenameScreenshots.Click += btnRenameScreenshots_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(19, 415);
            label7.Margin = new Padding(5, 0, 5, 0);
            label7.Name = "label7";
            label7.Size = new Size(211, 24);
            label7.TabIndex = 16;
            label7.Text = "格式: HH:MM:SS 或 秒数";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(241, 415);
            label8.Margin = new Padding(5, 0, 5, 0);
            label8.Name = "label8";
            label8.Size = new Size(211, 24);
            label8.TabIndex = 17;
            label8.Text = "格式: HH:MM:SS 或 秒数";
            // 
            // btnRemoveSegment
            // 
            btnRemoveSegment.Location = new Point(451, 343);
            btnRemoveSegment.Margin = new Padding(5);
            btnRemoveSegment.Name = "btnRemoveSegment";
            btnRemoveSegment.Size = new Size(406, 96);
            btnRemoveSegment.TabIndex = 17;
            btnRemoveSegment.Text = "删除中间段 (分段提取合并法)";
            btnRemoveSegment.UseVisualStyleBackColor = true;
            btnRemoveSegment.Click += btnRemoveSegment_Click;
            // 
            // btnFpsScreenshot
            // 
            btnFpsScreenshot.Location = new Point(19, 253);
            btnFpsScreenshot.Margin = new Padding(5);
            btnFpsScreenshot.Name = "btnFpsScreenshot";
            btnFpsScreenshot.Size = new Size(379, 61);
            btnFpsScreenshot.TabIndex = 18;
            btnFpsScreenshot.Text = "使用FPS滤镜生成截图 (硬件加速)";
            btnFpsScreenshot.UseVisualStyleBackColor = true;
            btnFpsScreenshot.Click += btnFpsScreenshot_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(602, 12);
            label9.Margin = new Padding(5, 0, 5, 0);
            label9.Name = "label9";
            label9.Size = new Size(86, 24);
            label9.TabIndex = 19;
            label9.Text = "主题设置:";
            // 
            // cmbTheme
            // 
            cmbTheme.Items.AddRange(new object[] { "明亮主题", "黑暗主题" });
            cmbTheme.Location = new Point(706, 12);
            cmbTheme.Margin = new Padding(5);
            cmbTheme.Name = "cmbTheme";
            cmbTheme.Size = new Size(169, 32);
            cmbTheme.TabIndex = 20;
            cmbTheme.SelectedIndexChanged += cmbTheme_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(878, 559);
            Controls.Add(label9);
            Controls.Add(cmbTheme);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(btnRemoveSegment);
            Controls.Add(btnFpsScreenshot);
            Controls.Add(btnRenameScreenshots);
            Controls.Add(txtEndTime);
            Controls.Add(txtStartTime);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(progressBar1);
            Controls.Add(txtOutputPath);
            Controls.Add(btnBrowseOutput);
            Controls.Add(label3);
            Controls.Add(numInterval);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtVideoPath);
            Controls.Add(btnBrowseVideo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(5);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "视频处理工具";
            FormClosing += Form1_FormClosing;
            ((System.ComponentModel.ISupportInitialize)numInterval).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        private Button btnBrowseVideo;
        private TextBox txtVideoPath;
        private Label label1;
        private Label label2;
        private NumericUpDown numInterval;
        private Label label3;
        private Button btnBrowseOutput;
        private TextBox txtOutputPath;
        private Button btnFpsScreenshot;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtStartTime;
        private TextBox txtEndTime;
        private Button btnRenameScreenshots;
        private Button btnRemoveSegment;
        private Label label7;
        private Label label8;
        private ProgressBar progressBar1;
        private Label label9;
        private ComboBox cmbTheme;

        private void btnBrowseVideo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "视频文件|*.mp4;*.avi;*.mov;*.wmv;*.flv|所有文件|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtVideoPath.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "选择输出路径";
                folderBrowserDialog.UseDescriptionForTitle = true;

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputPath.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }





        private void RenameScreenshotsWithTime(string outputPath, string fileName, int interval)
        {
            // 查找包含时间间隔的临时文件
            string[] tempFiles = Directory.GetFiles(outputPath, $"{fileName}_temp_*s_*.jpg");
            WriteLog($"开始重命名截图文件 - 找到 {tempFiles.Length} 个临时文件");
            
            if (tempFiles.Length == 0)
            {
                // 如果没有找到，尝试使用旧的命名模式作为备选
                tempFiles = Directory.GetFiles(outputPath, $"{fileName}_temp_*.jpg");
                WriteLog($"尝试旧命名模式 - 找到 {tempFiles.Length} 个临时文件");
                
                if (tempFiles.Length == 0)
                {
                    // 尝试查找使用FPS滤镜生成的截图文件
                    tempFiles = Directory.GetFiles(outputPath, $"{fileName}_fps_*.jpg");
                    WriteLog($"尝试FPS滤镜命名模式 - 找到 {tempFiles.Length} 个临时文件");
                    
                    if (tempFiles.Length == 0)
                    {
                        WriteLog("没有找到需要重命名的临时文件");
                        return;
                    }
                }
            }

            // 按文件名中的数字排序，确保正确的顺序
            WriteLog("开始对临时文件进行排序");
            Array.Sort(tempFiles, (x, y) => 
            {
                // 提取文件名中的数字部分
                int xNum = ExtractNumberFromFileName(x);
                int yNum = ExtractNumberFromFileName(y);
                return xNum.CompareTo(yNum);
            });
            WriteLog("临时文件排序完成");

            WriteLog($"开始逐个重命名文件，共 {tempFiles.Length} 个文件");
            for (int i = 0; i < tempFiles.Length; i++)
            {
                int currentTime = i * interval;
                // 使用TimeSpan格式化时间为HH:MM:SS格式
                TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
                string timeSuffix = $"_{timeSpan.Hours:00}h_{timeSpan.Minutes:00}m_{timeSpan.Seconds:00}s.jpg";
                string newFileName = Path.Combine(outputPath, fileName + timeSuffix);
                string oldFileName = tempFiles[i];

                WriteLog($"准备重命名文件 {i+1}/{tempFiles.Length}");
                WriteLog($"  原文件: {oldFileName}");
                WriteLog($"  新文件: {newFileName}");
                WriteLog($"  对应时间: {timeSpan}");

                // 如果目标文件存在，先删除
                if (File.Exists(newFileName))
                {
                    WriteLog($"  目标文件已存在，将先删除");
                    File.Delete(newFileName);
                    WriteLog($"  已删除存在的目标文件");
                }

                // 重命名文件
                File.Move(oldFileName, newFileName);
                WriteLog($"  文件重命名成功");
            }
            WriteLog("所有截图文件重命名完成");
        }

        /// <summary>
        /// 从文件名中提取数字部分
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>提取的数字</returns>
        private int ExtractNumberFromFileName(string fileName)
        {
            // 获取文件名（不含路径）
            string name = Path.GetFileNameWithoutExtension(fileName);
            // 找到最后一个下划线的位置
            int lastUnderscoreIndex = name.LastIndexOf('_');
            if (lastUnderscoreIndex >= 0 && lastUnderscoreIndex < name.Length - 1)
            {
                // 提取下划线后的数字部分
                string numStr = name.Substring(lastUnderscoreIndex + 1);
                if (int.TryParse(numStr, out int num))
                {
                    return num;
                }
            }
            return 0;
        }



        private void LoadConfig()
        {
            try
            {
                if (File.Exists(configFilePath))
                {
                    string[] lines = File.ReadAllLines(configFilePath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            string key = parts[0].Trim();
                            string value = parts[1].Trim();

                            switch (key)
                            {
                                case "VideoPath":
                                    txtVideoPath.Text = value;
                                    break;
                                case "Interval":
                                    if (int.TryParse(value, out int interval))
                                    {
                                        numInterval.Value = interval;
                                    }
                                    break;
                                case "OutputPath":
                                    txtOutputPath.Text = value;
                                    break;
                                case "Theme":
                                    if (Enum.TryParse(value, out ThemeType theme))
                                    {
                                        currentTheme = theme;
                                        cmbTheme.SelectedIndex = currentTheme == ThemeType.Light ? 0 : 1;
                                    }
                                    break;
                            }
                        }
                    }
                }
                // 应用主题
                ApplyTheme(currentTheme);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"加载配置失败: {ex.Message}");
            }
        }

        private void SaveConfig()
        {
            try
            {
                string directory = Path.GetDirectoryName(configFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (StreamWriter writer = new StreamWriter(configFilePath))
                {
                    writer.WriteLine($"VideoPath={txtVideoPath.Text}");
                    writer.WriteLine($"Interval={numInterval.Value}");
                    writer.WriteLine($"OutputPath={txtOutputPath.Text}");
                    writer.WriteLine($"Theme={currentTheme.ToString()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"保存配置失败: {ex.Message}");
            }
        }



        private void btnRenameScreenshots_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtVideoPath.Text))
            {
                MessageBox.Show("请选择视频文件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtOutputPath.Text))
            {
                MessageBox.Show("请选择输出路径", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(txtOutputPath.Text))
            {
                MessageBox.Show("输出路径不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 设置UI状态
            progressBar1.Style = ProgressBarStyle.Marquee;
            btnRenameScreenshots.Enabled = false;
            btnRemoveSegment.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                // 获取必要的参数
                string videoPath = txtVideoPath.Text;
                int interval = (int)numInterval.Value;
                string outputPath = txtOutputPath.Text;
                string fileName = Path.GetFileNameWithoutExtension(videoPath);

                WriteLog($"开始直接重命名截图任务 - 视频文件: {videoPath}");
                WriteLog($"重命名参数 - 时间间隔: {interval}秒, 输出路径: {outputPath}");

                // 执行重命名操作
                RenameScreenshotsWithTime(outputPath, fileName, interval);
                WriteLog($"直接重命名截图任务完成");

                MessageBox.Show("截图重命名完成！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                WriteLog($"直接重命名截图失败: {ex.Message}");
                MessageBox.Show($"直接重命名截图失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 恢复UI状态
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Value = 0;
                btnRenameScreenshots.Enabled = true;
                btnRemoveSegment.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnRemoveSegment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtVideoPath.Text))
            {
                MessageBox.Show("请选择视频文件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtOutputPath.Text))
            {
                MessageBox.Show("请选择输出路径", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(txtVideoPath.Text))
            {
                MessageBox.Show("视频文件不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(txtOutputPath.Text))
            {
                MessageBox.Show("输出路径不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 验证时间格式
            string startTime = txtStartTime.Text.Trim();
            string endTime = txtEndTime.Text.Trim();

            if (string.IsNullOrWhiteSpace(startTime) || string.IsNullOrWhiteSpace(endTime))
            {
                MessageBox.Show("请输入开始时间和结束时间", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 设置UI状态
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Value = 0;
            btnRenameScreenshots.Enabled = false;
            btnRemoveSegment.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                // 直接执行视频剪辑逻辑
                string ffmpegPath = "C:\\ffmpeg-8.0.1-essentials_build\\bin\\ffmpeg.exe";
                string videoPath = txtVideoPath.Text;
                string outputPath = txtOutputPath.Text;
                string fileName = Path.GetFileNameWithoutExtension(videoPath);

                WriteLog($"开始视频剪辑任务（分段提取合并法） - 视频文件: {videoPath}");
                WriteLog($"剪辑参数 - 开始时间: {startTime}, 结束时间: {endTime}, 输出路径: {outputPath}");

                if (!File.Exists(ffmpegPath))
                {
                    WriteLog($"错误: 找不到ffmpeg，请确保路径正确: {ffmpegPath}");
                    throw new Exception($"找不到ffmpeg，请确保路径正确: {ffmpegPath}");
                }

                // 解析时间格式，确保转换为秒数
                double startTimeSeconds = ParseTimeToSeconds(startTime);
                double endTimeSeconds = ParseTimeToSeconds(endTime);

                // 生成输出文件名
                string outputFileName = Path.Combine(outputPath, $"{fileName}_merged.mp4");

                // 使用分段提取合并法删除中间段，添加进度反馈
                RemoveSegment(ffmpegPath, videoPath, outputFileName, startTimeSeconds, endTimeSeconds, progress => {
                    progressBar1.Value = progress;
                });
                WriteLog($"视频剪辑任务完成");

                MessageBox.Show("视频剪辑完成！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                WriteLog($"视频剪辑失败: {ex.Message}");
                MessageBox.Show($"视频剪辑失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 恢复UI状态
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Value = 0;
                btnRenameScreenshots.Enabled = true;
                btnRemoveSegment.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 将时间字符串转换为秒数
        /// </summary>
        /// <param name="timeStr">时间字符串，可以是秒数或HH:MM:SS格式</param>
        /// <returns>转换后的秒数</returns>
        private double ParseTimeToSeconds(string timeStr)
        {
            // 如果是纯数字，直接转换为秒数
            if (double.TryParse(timeStr, out double seconds))
            {
                return seconds;
            }

            // 否则尝试解析HH:MM:SS格式
            if (TimeSpan.TryParse(timeStr, out TimeSpan timeSpan))
            {
                return timeSpan.TotalSeconds;
            }

            // 如果都失败，抛出异常
            throw new Exception($"无法解析时间格式: {timeStr}");
        }

        private double GetVideoDuration(string ffmpegPath, string videoPath)
        {
            // 尝试使用ffprobe获取视频时长，这是更可靠的方式
            string arguments = $"-v quiet -select_streams v:0 -show_entries stream=duration -of csv=\"p=0\" \"{videoPath}\"";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = Path.Combine(Path.GetDirectoryName(ffmpegPath), "ffprobe.exe"),
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string errorOutput = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    // 如果ffprobe失败，尝试使用ffmpeg作为备选方案
                    return GetVideoDurationFallback(ffmpegPath, videoPath);
                }

                if (double.TryParse(output.Trim(), out double duration))
                {
                    return duration;
                }
                else
                {
                    // 如果解析失败，尝试使用ffmpeg作为备选方案
                    return GetVideoDurationFallback(ffmpegPath, videoPath);
                }
            }
        }

        private double GetVideoDurationFallback(string ffmpegPath, string videoPath)
        {
            // 使用ffmpeg作为备选方案获取视频时长
            string arguments = $"-i \"{videoPath}\" -f null -";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = ffmpegPath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.Start();
                string errorOutput = process.StandardError.ReadToEnd();
                process.WaitForExit();

                // 解析ffmpeg输出中的时长信息
                // 典型输出格式: Duration: 00:01:23.45, start: 0.000000, bitrate: 1234 kb/s
                string durationPattern = "Duration: (\\d{2}:\\d{2}:\\d{2}\\.\\d{2})";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(durationPattern);
                System.Text.RegularExpressions.Match match = regex.Match(errorOutput);

                if (match.Success)
                {
                    string durationStr = match.Groups[1].Value;
                    if (TimeSpan.TryParse(durationStr, out TimeSpan duration))
                    {
                        return duration.TotalSeconds;
                    }
                }

                // 如果所有方法都失败，返回一个默认值或抛出更友好的错误
                throw new Exception("无法获取视频时长，请检查视频文件是否损坏或格式不支持");
            }
        }

        /// <summary>
        /// 执行FFmpeg命令并提供进度反馈
        /// </summary>
        /// <param name="ffmpegPath">FFmpeg可执行文件路径</param>
        /// <param name="arguments">命令行参数</param>
        /// <param name="progressCallback">进度回调函数，接收0-100的进度值</param>
        private void ExecuteFfmpegCommand(string ffmpegPath, string arguments, Action<int> progressCallback = null)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = ffmpegPath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                
                // 异步读取输出流，避免缓冲区满导致阻塞
                StringBuilder outputBuilder = new StringBuilder();
                StringBuilder errorBuilder = new StringBuilder();
                
                using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
                {
                    process.OutputDataReceived += (sender, e) => 
                    {
                        if (e.Data == null)
                        {
                            outputWaitHandle.Set();
                        }
                        else
                        {
                            outputBuilder.AppendLine(e.Data);
                        }
                    };
                    
                    process.ErrorDataReceived += (sender, e) => 
                    {
                        if (e.Data == null)
                        {
                            errorWaitHandle.Set();
                        }
                        else
                        {
                            string line = e.Data;
                            errorBuilder.AppendLine(line);
                            
                            // 解析FFmpeg进度信息
                            if (progressCallback != null && line.Contains("time="))
                            {
                                try
                                {
                                    // 提取时间信息，格式如：time=00:00:05.00
                                    int timeIndex = line.IndexOf("time=") + 5;
                                    int nextSpaceIndex = line.IndexOf(' ', timeIndex);
                                    string timeStr = line.Substring(timeIndex, nextSpaceIndex - timeIndex);
                                    
                                    // 解析总时长（从参数中提取）
                                    double totalDuration = 0;
                                    if (arguments.Contains("-t "))
                                    {
                                        // 提取-t参数值
                                        int tIndex = arguments.IndexOf("-t ") + 3;
                                        int tNextSpace = arguments.IndexOf(' ', tIndex);
                                        if (tNextSpace > 0)
                                        {
                                            string tValue = arguments.Substring(tIndex, tNextSpace - tIndex);
                                            double.TryParse(tValue, out totalDuration);
                                        }
                                    }
                                    
                                    // 将时间字符串转换为秒数
                                    if (TimeSpan.TryParse(timeStr, out TimeSpan currentTime))
                                    {
                                        if (totalDuration > 0)
                                        {
                                            // 计算进度百分比
                                            int progress = (int)((currentTime.TotalSeconds / totalDuration) * 100);
                                            // 确保进度值在0-100之间
                                            progress = Math.Max(0, Math.Min(100, progress));
                                            
                                            // 使用Invoke更新UI，避免跨线程异常
                                            this.Invoke(new Action(() => {
                                                progressCallback(progress);
                                            }));
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    // 进度解析失败，忽略并继续
                                    WriteLog($"进度解析失败: {ex.Message}");
                                }
                            }
                        }
                    };
                    
                    process.Start();
                    WriteLog($"ffmpeg进程已启动，执行命令: {arguments}");
                    
                    // 开始异步读取
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    
                    // 等待进程退出和所有输出被读取
                    if (process.WaitForExit(1800000)) // 设置30分钟超时，足够处理1小时的视频
                    {
                        // 等待输出流读取完成
                        outputWaitHandle.WaitOne(2000); // 稍微延长等待时间，确保所有输出都被读取
                        errorWaitHandle.WaitOne(2000);
                        
                        if (process.ExitCode != 0)
                        {
                            string errorOutput = errorBuilder.ToString();
                            WriteLog($"错误: ffmpeg执行失败，退出码: {process.ExitCode}");
                            WriteLog($"ffmpeg错误输出: {errorOutput}");
                            throw new Exception($"ffmpeg执行失败: {errorOutput}\n命令: {arguments}");
                        }
                        else
                        {
                            WriteLog($"ffmpeg执行成功，退出码: {process.ExitCode}");
                            // 记录一些ffmpeg输出，帮助调试
                            string fullOutput = outputBuilder.ToString();
                            string fullError = errorBuilder.ToString();
                            if (!string.IsNullOrWhiteSpace(fullOutput))
                            {
                                WriteLog($"ffmpeg标准输出: {fullOutput.Substring(0, Math.Min(1000, fullOutput.Length))}..."); // 只记录前1000字符
                            }
                            if (!string.IsNullOrWhiteSpace(fullError))
                            {
                                WriteLog($"ffmpeg错误输出: {fullError.Substring(0, Math.Min(1000, fullError.Length))}..."); // 只记录前1000字符
                            }
                        }
                    }
                    else
                    {
                        // 超时处理
                        process.Kill();
                        WriteLog($"错误: ffmpeg执行超时（30分钟）");
                        throw new Exception($"ffmpeg执行超时（30分钟）\n命令: {arguments}");
                    }
                }
            }
        }

        private string ParseTimeFormat(string timeStr)
        {
            // 如果是纯数字，直接返回（秒数格式）
            if (double.TryParse(timeStr, out _))
            {
                return timeStr;
            }
            // 否则假设是HH:MM:SS格式，直接返回
            return timeStr;
        }



        /// <summary>
        /// 删除视频中间的一段 (n秒 到 m秒)，使用分段提取合并法
        /// </summary>
        /// <param name="ffmpegPath">FFmpeg可执行文件路径</param>
        /// <param name="inputPath">输入视频文件路径</param>
        /// <param name="outputPath">输出视频文件路径</param>
        /// <param name="n">删除开始时间（秒）</param>
        /// <param name="m">删除结束时间（秒）</param>
        /// <param name="progressCallback">进度回调函数，接收0-100的进度值</param>
        private void RemoveSegment(string ffmpegPath, string inputPath, string outputPath, double n, double m, Action<int> progressCallback = null)
        {
            if (!File.Exists(inputPath))
                throw new FileNotFoundException("找不到输入文件", inputPath);

            if (n >= m)
                throw new ArgumentException("删除结束时间必须大于开始时间");

            // 生成唯一ID，用于临时文件名，避免冲突
            string uniqueId = Guid.NewGuid().ToString("N");
            string directory = Path.GetDirectoryName(outputPath);
            string part1Path = Path.Combine(directory, $"temp_part1_{uniqueId}.mp4");
            string part2Path = Path.Combine(directory, $"temp_part2_{uniqueId}.mp4");
            string listPath = Path.Combine(directory, $"temp_list_{uniqueId}.txt");

            try
            {
                // 获取视频总时长，用于计算整体进度
                double totalDuration = GetVideoDuration(ffmpegPath, inputPath);
                
                // 1. 提取前半段 (0 -> n)
                // -hwaccel auto: 启用硬件加速
                // -ss 0: 开始时间
                // -t n: 持续时长
                // -c copy: 流复制(不重新编码，速度快)
                // -avoid_negative_ts 1: 修正时间戳
                WriteLog($"正在提取前半段 (0 - {n}s)...");
                string arguments1 = $"-hwaccel auto -y -i \"{inputPath}\" -t {n} -c copy -avoid_negative_ts 1 \"{part1Path}\"";
                ExecuteFfmpegCommand(ffmpegPath, arguments1, progress => {
                    // 前半段提取占总进度的33%
                    progressCallback?.Invoke((int)(progress * 0.33));
                });

                // 2. 提取后半段 (m -> end)
                // -hwaccel auto: 启用硬件加速
                // -ss m: 开始时间
                // -c copy: 流复制(不重新编码，速度快)
                WriteLog($"正在提取后半段 ({m}s - End)...");
                string arguments2 = $"-hwaccel auto -y -i \"{inputPath}\" -ss {m} -c copy -avoid_negative_ts 1 \"{part2Path}\"";
                ExecuteFfmpegCommand(ffmpegPath, arguments2, progress => {
                    // 后半段提取占总进度的33%（33%-66%）
                    progressCallback?.Invoke(33 + (int)(progress * 0.33));
                });

                // 3. 创建合并列表文件
                // FFmpeg concat demuxer 需要一个文本文件列出要合并的视频
                // 格式为: file '路径'
                var sb = new StringBuilder();
                sb.AppendLine($"file '{part1Path}'");
                sb.AppendLine($"file '{part2Path}'");
                File.WriteAllText(listPath, sb.ToString());
                WriteLog($"已创建合并列表文件: {listPath}");

                // 4. 合并视频
                // -hwaccel auto: 启用硬件加速
                // -f concat: 使用合并解复用器
                // -safe 0: 允许使用绝对路径
                // -c copy: 流复制(不重新编码，速度快)
                // 添加重新编码选项，如果流复制失败则使用重新编码
                WriteLog("正在合并视频...");
                string arguments3 = $"-hwaccel auto -y -f concat -safe 0 -i \"{listPath}\" -c copy \"{outputPath}\"";
                
                try
                {
                    ExecuteFfmpegCommand(ffmpegPath, arguments3, progress => {
                        // 合并占总进度的34%（66%-100%）
                        progressCallback?.Invoke(66 + (int)(progress * 0.34));
                    });
                }
                catch (Exception ex)
                {
                    // 如果流复制失败，尝试重新编码
                    WriteLog($"流复制失败，尝试重新编码: {ex.Message}");
                    arguments3 = $"-hwaccel auto -y -f concat -safe 0 -i \"{listPath}\" -c:v libx264 -c:a aac \"{outputPath}\"";
                    ExecuteFfmpegCommand(ffmpegPath, arguments3, progress => {
                        // 重新编码占总进度的34%（66%-100%）
                        progressCallback?.Invoke(66 + (int)(progress * 0.34));
                    });
                }
            }
            finally
            {
                // 5. 清理临时文件
                WriteLog("清理临时文件...");
                DeleteFileIfExists(part1Path);
                DeleteFileIfExists(part2Path);
                DeleteFileIfExists(listPath);
            }
        }

        /// <summary>
        /// 删除文件，如果不存在则忽略
        /// </summary>
        private void DeleteFileIfExists(string path)
        {
            if (File.Exists(path))
            {
                try { File.Delete(path); } catch { }
            }
        }

        private void btnFpsScreenshot_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtVideoPath.Text))
            {
                MessageBox.Show("请选择视频文件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtOutputPath.Text))
            {
                MessageBox.Show("请选择输出路径", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(txtVideoPath.Text))
            {
                MessageBox.Show("视频文件不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(txtOutputPath.Text))
            {
                MessageBox.Show("输出路径不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 设置UI状态
            progressBar1.Style = ProgressBarStyle.Marquee;
            btnRenameScreenshots.Enabled = false;
            btnRemoveSegment.Enabled = false;
            btnFpsScreenshot.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                // 直接执行截图逻辑
                string ffmpegPath = "C:\\ffmpeg-8.0.1-essentials_build\\bin\\ffmpeg.exe";
                string videoPath = txtVideoPath.Text;
                int interval = (int)numInterval.Value;
                string outputPath = txtOutputPath.Text;
                string fileName = Path.GetFileNameWithoutExtension(videoPath);

                WriteLog($"开始生成截图任务（FPS滤镜） - 视频文件: {videoPath}");
                WriteLog($"截图参数 - 时间间隔: {interval}秒, 输出路径: {outputPath}");

                if (!File.Exists(ffmpegPath))
                {
                    WriteLog($"错误: 找不到ffmpeg，请确保路径正确: {ffmpegPath}");
                    throw new Exception($"找不到ffmpeg，请确保路径正确: {ffmpegPath}");
                }

                // 1. 确保输出目录存在
                if (!Directory.Exists(outputPath))
                {
                    Directory.CreateDirectory(outputPath);
                }

                // 2. 构造输出文件名模式
                string outputPattern = Path.Combine(outputPath, $"{fileName}_fps_%04d.jpg");

                // 3. 构造 FFmpeg 命令参数
                // -hwaccel auto: 尝试使用硬件加速解码
                // -i: 输入文件
                // -vf fps=1/{interval}: 视频滤镜，1/{interval} 表示每N秒取一帧
                // -q:v 2: 输出图片质量 (JPG)，2-5 之间质量很好且体积适中 (1最高，31最低)
                // -an: 禁用音频处理 (加快速度)
                string fpsFilter = $"fps=1/{interval}";
                string arguments = $"-hwaccel auto -i \"{videoPath}\" -vf {fpsFilter} -q:v 2 -an \"{outputPattern}\"";

                WriteLog($"准备执行ffmpeg命令 - 命令行: {ffmpegPath} {arguments}");
                ExecuteFfmpegCommand(ffmpegPath, arguments);
                WriteLog($"ffmpeg截图命令执行完成");

                // 4. 重命名文件，添加时间后缀
                WriteLog($"开始重命名截图文件，添加时间后缀");
                RenameScreenshotsWithTime(outputPath, fileName, interval);
                WriteLog($"截图生成任务完成");

                MessageBox.Show("截图生成完成！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                WriteLog($"执行失败: {ex.Message}");
                MessageBox.Show($"执行失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 恢复UI状态
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Value = 0;
                btnRenameScreenshots.Enabled = true;
                btnRemoveSegment.Enabled = true;
                btnFpsScreenshot.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
        }
        
        /// <summary>
        /// 主题选择变更事件处理
        /// </summary>
        private void cmbTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentTheme = cmbTheme.SelectedIndex == 0 ? ThemeType.Light : ThemeType.Dark;
            ApplyTheme(currentTheme);
        }
        
        /// <summary>
        /// 应用主题
        /// </summary>
        /// <param name="theme">主题类型</param>
        private void ApplyTheme(ThemeType theme)
        {
            if (theme == ThemeType.Light)
            {
                // 明亮主题颜色
                BackColor = SystemColors.Control;
                ForeColor = SystemColors.ControlText;
                
                // 文本框主题
                foreach (Control control in Controls)
                {
                    if (control is TextBox textBox)
                    {
                        textBox.BackColor = Color.White;
                        textBox.ForeColor = SystemColors.ControlText;
                        textBox.BorderStyle = BorderStyle.FixedSingle;
                    }
                    else if (control is NumericUpDown numericUpDown)
                    {
                        numericUpDown.BackColor = Color.White;
                        numericUpDown.ForeColor = SystemColors.ControlText;
                    }
                    else if (control is ComboBox comboBox)
                    {
                        comboBox.BackColor = Color.White;
                        comboBox.ForeColor = SystemColors.ControlText;
                        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    }
                    else if (control is Button button)
                    {
                        button.BackColor = SystemColors.Control;
                        button.ForeColor = SystemColors.ControlText;
                        button.FlatStyle = FlatStyle.Standard;
                    }
                    else if (control is Label label)
                    {
                        label.ForeColor = SystemColors.ControlText;
                    }
                    else if (control is ProgressBar progressBar)
                    {
                        progressBar.BackColor = SystemColors.Control;
                        // 进度条本身颜色由系统主题控制
                    }
                }
            }
            else
            {
                // 黑暗主题颜色
                BackColor = Color.FromArgb(30, 30, 30);
                ForeColor = Color.White;
                
                // 文本框主题
                foreach (Control control in Controls)
                {
                    if (control is TextBox textBox)
                    {
                        textBox.BackColor = Color.FromArgb(60, 60, 60);
                        textBox.ForeColor = Color.White;
                        textBox.BorderStyle = BorderStyle.FixedSingle;
                    }
                    else if (control is NumericUpDown numericUpDown)
                    {
                        numericUpDown.BackColor = Color.FromArgb(60, 60, 60);
                        numericUpDown.ForeColor = Color.White;
                    }
                    else if (control is ComboBox comboBox)
                    {
                        comboBox.BackColor = Color.FromArgb(60, 60, 60);
                        comboBox.ForeColor = Color.White;
                        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    }
                    else if (control is Button button)
                    {
                        button.BackColor = Color.FromArgb(60, 60, 60);
                        button.ForeColor = Color.White;
                        button.FlatStyle = FlatStyle.Flat;
                        button.FlatAppearance.BorderColor = Color.FromArgb(100, 100, 100);
                        button.FlatAppearance.BorderSize = 1;
                    }
                    else if (control is Label label)
                    {
                        label.ForeColor = Color.White;
                    }
                    else if (control is ProgressBar progressBar)
                    {
                        progressBar.BackColor = Color.FromArgb(60, 60, 60);
                    }
                }
            }
        }
    }
}