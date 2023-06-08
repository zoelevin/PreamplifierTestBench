using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


namespace UA_GUI
{
    public class EventTest
    {
        private static System.Timers.Timer popUpTimer = new System.Timers.Timer(500);

        public delegate void Del(Form parentForm, int[] error_codes);
        public delegate void LoadDel(Form parentForm);
        public delegate void ProcessResultDelegate(int[] error_codes = null);
       // public static System.Timers.Timer myTimer 
           // = new System.Timers.Timer();
    
        static int errorFormOpened = 0;
        static Form parentform;
        // Create a timer
        public EventTest(Form pForm)
            
        {
            parentform = pForm;
            SetTimer();


            // Tell the timer what to do when it elapses
           // myTimer.Elapsed += ProcessResult;
            ;

            // Set it to go off every five seconds
           // myTimer.Interval = 5000;
            // And start it        
           // myTimer.Enabled = true;


        }



        // Implement a call with the right signature for events going off
        public void ProcessResult(params int[] errors) {
            //myTimer.Enabled = false;
            Control control = (Control)parentform;
            if (errors.Count() == 0) //load form
            {

                
                if (control.InvokeRequired)
                {
                    ProcessResultDelegate eForm = ProcessResult;
                    control.Invoke(eForm, new object[] { errors });  // invoking itself
                }
                else
                {
                    LoadDel callform = MakeProductSelect;
                    control.Invoke(callform, parentform);
                }
            }
            else
            {
                if (errorFormOpened == 0) //error form
                {
                    
                    if (control.InvokeRequired)
                    {
                        ProcessResultDelegate eForm = ProcessResult;
                        control.Invoke(eForm, new object[] { errors });  // invoking itself
                    }
                    else
                    {
                        Del callForm = MakeErrorForm; 
                        //int[] errors = new int[] {  4, 6 };
                        //var invoker = new Del(MakeErrorForm);
                        control.Invoke(callForm, new object[] { parentform, errors });// the "functional part", executing only on the main thread
                    }
                    errorFormOpened = 1;
                    SetTimer();
                }
            }

           
           
        }

        public static void MakeErrorForm(Form parentForm, int[] error_codes)
        {
            int x = 0;
            foreach (int error_code in error_codes)
            {
                
                Form errorform = new ErrorForm(parentForm, error_code);
                //do not use Show() on triggered events... not sure why 
                //i think errorform needs to run on a background thread
                //Invoke((Action)(() => { saveFileDialog.ShowDialog() }));
                if (error_code == error_codes[0] )
                {
                    foreach(Control ctl in errorform.Controls)
                        if (ctl.Name == "close_Btn" )
                        {
                            ctl.Hide();
                        }

                }
                errorform.Show(); //this allows for two forms to show up
                //errorform.Dispose();
            }


        }

        public static void MakeProductSelect(Form parentForm)
        {
            Form selectForm = new ProductSelect();
            selectForm.Show();
            foreach (Form frm in System.Windows.Forms.Application.OpenForms)
            {
                if (frm.Name == "LoadForm")   //close the load form, open next form
                {

                    frm.Hide();
                }
            }
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (errorFormOpened == 1)
            {
                errorFormOpened = 0;
            }

        }
        private static void SetTimer()
        {
            // Create a timer with a 300 ms interval.
            popUpTimer = new System.Timers.Timer(500);
            // Hook up the Elapsed event for the timer. 
            popUpTimer.Elapsed += OnTimedEvent;
            popUpTimer.AutoReset = false;
            popUpTimer.Enabled = true;
        }

    }
}
