using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace StableDiffusionUnity
{
    [System.Serializable]
    public class TextToImageMessage
    {
        //   {
        //   "enable_hr": false,
        //   "denoising_strength": 0,
        //   "firstphase_width": 0,
        //   "firstphase_height": 0,
        //   "hr_scale": 2,
        //   "hr_upscaler": "string",
        //   "hr_second_pass_steps": 0,
        //   "hr_resize_x": 0,
        //   "hr_resize_y": 0,
        //   "prompt": "",
        //   "styles": [
        //     "string"
        //   ],
        //   "seed": -1,
        //   "subseed": -1,
        //   "subseed_strength": 0,
        //   "seed_resize_from_h": -1,
        //   "seed_resize_from_w": -1,
        //   "sampler_name": "string",
        //   "batch_size": 1,
        //   "n_iter": 1,
        //   "steps": 50,
        //   "cfg_scale": 7,
        //   "width": 512,
        //   "height": 512,
        //   "restore_faces": false,
        //   "tiling": false,
        //   "do_not_save_samples": false,
        //   "do_not_save_grid": false,
        //   "negative_prompt": "string",
        //   "eta": 0,
        //   "s_churn": 0,
        //   "s_tmax": 0,
        //   "s_tmin": 0,
        //   "s_noise": 1,
        //   "override_settings": {},
        //   "override_settings_restore_afterwards": true,
        //   "script_args": [],
        //   "sampler_index": "Euler",
        //   "script_name": "string",
        //   "send_images": true,
        //   "save_images": false,
        //   "alwayson_scripts": {}
        // }

        public bool enable_hr = false;
        public int denoising_strength = 0;
        public int firstphase_width = 0;
        public int firstphase_height = 0;
        public int hr_scale = 2;
        public string hr_upscaler = "None";
        public int hr_second_pass_steps = 0;
        public int hr_resize_x = 0;
        public int hr_resize_y = 0;
        public string prompt = "";
        // public List<string> styles = new List<string>();
        public int seed = -1;
        public int subseed = -1;
        public int subseed_strength = 0;
        public int seed_resize_from_h = -1;
        public int seed_resize_from_w = -1;
        // public string sampler_name = "Euler";
        public int batch_size = 1;
        public int n_iter = 1;
        public int steps = 20;
        public int cfg_scale = 7;
        public int width = 512;
        public int height = 512;
        // public bool restore_faces = false;
        public bool tiling = false;
        // public bool do_not_save_samples = false;
        // public bool do_not_save_grid = false;
        // public string negative_prompt = "";
        // public float eta = 0;
        // public float s_churn = 0;
        // public float s_tmax = 0;
        // public float s_tmin = 0;
        // public float s_noise = 1;
        // public Dictionary<string, string> override_settings = new Dictionary<string, string>();
        // public bool override_settings_restore_afterwards = true;
        // public List<string> script_args = new List<string>();
        // public string sampler_index = "Euler";
        // public string script_name = "string";
        // public bool send_images = true;
        // public bool save_images = false;
        // public Dictionary<string, string> alwayson_scripts = new Dictionary<string, string>();

        public override string ToString()
        {
            // convert to json using newtonsoft
            return JsonConvert.SerializeObject(this);
        }
    }
}
