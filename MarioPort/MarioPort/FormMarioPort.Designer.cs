﻿namespace MarioPort
{
   partial class FormMarioPort
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
         this.SuspendLayout();
         // 
         // FormMarioPort
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(442, 383);
         this.DoubleBuffered = true;
         this.Name = "FormMarioPort";
         this.Text = "MarioPort";
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMarioPort_FormClosing);
         this.Load += new System.EventHandler(this.FormMarioPort_Load);
         this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMarioPort_KeyDown);
         this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMarioPort_KeyUp);
         this.ResumeLayout(false);

      }

      #endregion
   }
}

