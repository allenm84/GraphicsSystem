namespace MLA.Graphics.WinForms
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.screen = new System.Windows.Forms.PictureBox();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.btnEllipse = new System.Windows.Forms.Button();
      this.btnRectangle = new System.Windows.Forms.Button();
      this.btnPolygon = new System.Windows.Forms.Button();
      this.btnClear = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.screen)).BeginInit();
      this.tableLayoutPanel1.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // screen
      // 
      this.screen.Dock = System.Windows.Forms.DockStyle.Fill;
      this.screen.Location = new System.Drawing.Point(0, 0);
      this.screen.Margin = new System.Windows.Forms.Padding(0);
      this.screen.Name = "screen";
      this.screen.Size = new System.Drawing.Size(393, 328);
      this.screen.TabIndex = 0;
      this.screen.TabStop = false;
      this.screen.SizeChanged += new System.EventHandler(this.screen_SizeChanged);
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
      this.tableLayoutPanel1.Controls.Add(this.screen, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(473, 328);
      this.tableLayoutPanel1.TabIndex = 1;
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 1;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Controls.Add(this.btnEllipse, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.btnRectangle, 0, 1);
      this.tableLayoutPanel2.Controls.Add(this.btnPolygon, 0, 2);
      this.tableLayoutPanel2.Controls.Add(this.btnClear, 0, 4);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(393, 0);
      this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 5;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(80, 328);
      this.tableLayoutPanel2.TabIndex = 1;
      // 
      // btnEllipse
      // 
      this.btnEllipse.Location = new System.Drawing.Point(3, 3);
      this.btnEllipse.Name = "btnEllipse";
      this.btnEllipse.Size = new System.Drawing.Size(74, 23);
      this.btnEllipse.TabIndex = 0;
      this.btnEllipse.Text = "Ellipse";
      this.btnEllipse.UseVisualStyleBackColor = true;
      this.btnEllipse.Click += new System.EventHandler(this.btnEllipse_Click);
      // 
      // btnRectangle
      // 
      this.btnRectangle.Location = new System.Drawing.Point(3, 33);
      this.btnRectangle.Name = "btnRectangle";
      this.btnRectangle.Size = new System.Drawing.Size(74, 23);
      this.btnRectangle.TabIndex = 1;
      this.btnRectangle.Text = "Rectangle";
      this.btnRectangle.UseVisualStyleBackColor = true;
      this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
      // 
      // btnPolygon
      // 
      this.btnPolygon.Location = new System.Drawing.Point(3, 63);
      this.btnPolygon.Name = "btnPolygon";
      this.btnPolygon.Size = new System.Drawing.Size(74, 23);
      this.btnPolygon.TabIndex = 2;
      this.btnPolygon.Text = "Polygon";
      this.btnPolygon.UseVisualStyleBackColor = true;
      this.btnPolygon.Click += new System.EventHandler(this.btnPolygon_Click);
      // 
      // btnClear
      // 
      this.btnClear.Location = new System.Drawing.Point(3, 301);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new System.Drawing.Size(74, 23);
      this.btnClear.TabIndex = 4;
      this.btnClear.Text = "Clear";
      this.btnClear.UseVisualStyleBackColor = true;
      this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(493, 348);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "Form1";
      this.Padding = new System.Windows.Forms.Padding(10);
      this.Text = "Form1";
      ((System.ComponentModel.ISupportInitialize)(this.screen)).EndInit();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox screen;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.Button btnEllipse;
    private System.Windows.Forms.Button btnRectangle;
    private System.Windows.Forms.Button btnPolygon;
    private System.Windows.Forms.Button btnClear;
  }
}

