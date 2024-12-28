//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.FrankaInterface
{
    [Serializable]
    public class RobotStateMsg : Message
    {
        public const string k_RosMessageName = "franka_interface_msgs/RobotState";
        public override string RosMessageName => k_RosMessageName;

        //  Franka robot state
        //  For more information about each field, look at robot_state.h here: 
        //  https://github.com/frankaemika/libfranka/blob/master/include/franka/robot_state.h
        public Std.HeaderMsg header;
        public double[] pose_desired;
        public double[] O_T_EE;
        public double[] O_T_EE_d;
        public double[] F_T_EE;
        public double[] EE_T_K;
        public double m_ee;
        public double[] I_ee;
        public double[] F_x_Cee;
        public double m_load;
        public double[] I_load;
        public double[] F_x_Cload;
        public double m_total;
        public double[] I_total;
        public double[] F_x_Ctotal;
        public double[] elbow;
        public double[] elbow_d;
        public double[] elbow_c;
        public double[] delbow_c;
        public double[] ddelbow_c;
        public double[] tau_J;
        public double[] tau_J_d;
        public double[] dtau_J;
        public double[] q;
        public double[] q_d;
        public double[] dq;
        public double[] dq_d;
        public double[] ddq_d;
        public double[] joint_contact;
        public double[] cartesian_contact;
        public double[] joint_collision;
        public double[] cartesian_collision;
        public double[] tau_ext_hat_filtered;
        public double[] O_F_ext_hat_K;
        public double[] K_F_ext_hat_K;
        public double[] O_dP_EE_d;
        public double[] O_T_EE_c;
        public double[] O_dP_EE_c;
        public double[] O_ddP_EE_c;
        public double[] theta;
        public double[] dtheta;
        public ErrorsMsg current_errors;
        public ErrorsMsg last_motion_errors;
        public double control_command_success_rate;
        public const byte ROBOT_MODE_OTHER = 0;
        public const byte ROBOT_MODE_IDLE = 1;
        public const byte ROBOT_MODE_MOVE = 2;
        public const byte ROBOT_MODE_GUIDING = 3;
        public const byte ROBOT_MODE_REFLEX = 4;
        public const byte ROBOT_MODE_USER_STOPPED = 5;
        public const byte ROBOT_MODE_AUTOMATIC_ERROR_RECOVERY = 6;
        public byte robot_mode;
        public double robot_time;
        public double gripper_width;
        public double gripper_max_width;
        public bool gripper_is_grasped;
        public ushort gripper_temperature;
        public double gripper_time;
        public bool is_fresh;

        public RobotStateMsg()
        {
            this.header = new Std.HeaderMsg();
            this.pose_desired = new double[16];
            this.O_T_EE = new double[16];
            this.O_T_EE_d = new double[16];
            this.F_T_EE = new double[16];
            this.EE_T_K = new double[16];
            this.m_ee = 0.0;
            this.I_ee = new double[9];
            this.F_x_Cee = new double[3];
            this.m_load = 0.0;
            this.I_load = new double[9];
            this.F_x_Cload = new double[3];
            this.m_total = 0.0;
            this.I_total = new double[9];
            this.F_x_Ctotal = new double[3];
            this.elbow = new double[2];
            this.elbow_d = new double[2];
            this.elbow_c = new double[2];
            this.delbow_c = new double[2];
            this.ddelbow_c = new double[2];
            this.tau_J = new double[7];
            this.tau_J_d = new double[7];
            this.dtau_J = new double[7];
            this.q = new double[7];
            this.q_d = new double[7];
            this.dq = new double[7];
            this.dq_d = new double[7];
            this.ddq_d = new double[7];
            this.joint_contact = new double[7];
            this.cartesian_contact = new double[6];
            this.joint_collision = new double[7];
            this.cartesian_collision = new double[6];
            this.tau_ext_hat_filtered = new double[7];
            this.O_F_ext_hat_K = new double[6];
            this.K_F_ext_hat_K = new double[6];
            this.O_dP_EE_d = new double[6];
            this.O_T_EE_c = new double[16];
            this.O_dP_EE_c = new double[6];
            this.O_ddP_EE_c = new double[6];
            this.theta = new double[7];
            this.dtheta = new double[7];
            this.current_errors = new ErrorsMsg();
            this.last_motion_errors = new ErrorsMsg();
            this.control_command_success_rate = 0.0;
            this.robot_mode = 0;
            this.robot_time = 0.0;
            this.gripper_width = 0.0;
            this.gripper_max_width = 0.0;
            this.gripper_is_grasped = false;
            this.gripper_temperature = 0;
            this.gripper_time = 0.0;
            this.is_fresh = false;
        }

        public RobotStateMsg(Std.HeaderMsg header, double[] pose_desired, double[] O_T_EE, double[] O_T_EE_d, double[] F_T_EE, double[] EE_T_K, double m_ee, double[] I_ee, double[] F_x_Cee, double m_load, double[] I_load, double[] F_x_Cload, double m_total, double[] I_total, double[] F_x_Ctotal, double[] elbow, double[] elbow_d, double[] elbow_c, double[] delbow_c, double[] ddelbow_c, double[] tau_J, double[] tau_J_d, double[] dtau_J, double[] q, double[] q_d, double[] dq, double[] dq_d, double[] ddq_d, double[] joint_contact, double[] cartesian_contact, double[] joint_collision, double[] cartesian_collision, double[] tau_ext_hat_filtered, double[] O_F_ext_hat_K, double[] K_F_ext_hat_K, double[] O_dP_EE_d, double[] O_T_EE_c, double[] O_dP_EE_c, double[] O_ddP_EE_c, double[] theta, double[] dtheta, ErrorsMsg current_errors, ErrorsMsg last_motion_errors, double control_command_success_rate, byte robot_mode, double robot_time, double gripper_width, double gripper_max_width, bool gripper_is_grasped, ushort gripper_temperature, double gripper_time, bool is_fresh)
        {
            this.header = header;
            this.pose_desired = pose_desired;
            this.O_T_EE = O_T_EE;
            this.O_T_EE_d = O_T_EE_d;
            this.F_T_EE = F_T_EE;
            this.EE_T_K = EE_T_K;
            this.m_ee = m_ee;
            this.I_ee = I_ee;
            this.F_x_Cee = F_x_Cee;
            this.m_load = m_load;
            this.I_load = I_load;
            this.F_x_Cload = F_x_Cload;
            this.m_total = m_total;
            this.I_total = I_total;
            this.F_x_Ctotal = F_x_Ctotal;
            this.elbow = elbow;
            this.elbow_d = elbow_d;
            this.elbow_c = elbow_c;
            this.delbow_c = delbow_c;
            this.ddelbow_c = ddelbow_c;
            this.tau_J = tau_J;
            this.tau_J_d = tau_J_d;
            this.dtau_J = dtau_J;
            this.q = q;
            this.q_d = q_d;
            this.dq = dq;
            this.dq_d = dq_d;
            this.ddq_d = ddq_d;
            this.joint_contact = joint_contact;
            this.cartesian_contact = cartesian_contact;
            this.joint_collision = joint_collision;
            this.cartesian_collision = cartesian_collision;
            this.tau_ext_hat_filtered = tau_ext_hat_filtered;
            this.O_F_ext_hat_K = O_F_ext_hat_K;
            this.K_F_ext_hat_K = K_F_ext_hat_K;
            this.O_dP_EE_d = O_dP_EE_d;
            this.O_T_EE_c = O_T_EE_c;
            this.O_dP_EE_c = O_dP_EE_c;
            this.O_ddP_EE_c = O_ddP_EE_c;
            this.theta = theta;
            this.dtheta = dtheta;
            this.current_errors = current_errors;
            this.last_motion_errors = last_motion_errors;
            this.control_command_success_rate = control_command_success_rate;
            this.robot_mode = robot_mode;
            this.robot_time = robot_time;
            this.gripper_width = gripper_width;
            this.gripper_max_width = gripper_max_width;
            this.gripper_is_grasped = gripper_is_grasped;
            this.gripper_temperature = gripper_temperature;
            this.gripper_time = gripper_time;
            this.is_fresh = is_fresh;
        }

        public static RobotStateMsg Deserialize(MessageDeserializer deserializer) => new RobotStateMsg(deserializer);

        private RobotStateMsg(MessageDeserializer deserializer)
        {
            this.header = Std.HeaderMsg.Deserialize(deserializer);
            deserializer.Read(out this.pose_desired, sizeof(double), 16);
            deserializer.Read(out this.O_T_EE, sizeof(double), 16);
            deserializer.Read(out this.O_T_EE_d, sizeof(double), 16);
            deserializer.Read(out this.F_T_EE, sizeof(double), 16);
            deserializer.Read(out this.EE_T_K, sizeof(double), 16);
            deserializer.Read(out this.m_ee);
            deserializer.Read(out this.I_ee, sizeof(double), 9);
            deserializer.Read(out this.F_x_Cee, sizeof(double), 3);
            deserializer.Read(out this.m_load);
            deserializer.Read(out this.I_load, sizeof(double), 9);
            deserializer.Read(out this.F_x_Cload, sizeof(double), 3);
            deserializer.Read(out this.m_total);
            deserializer.Read(out this.I_total, sizeof(double), 9);
            deserializer.Read(out this.F_x_Ctotal, sizeof(double), 3);
            deserializer.Read(out this.elbow, sizeof(double), 2);
            deserializer.Read(out this.elbow_d, sizeof(double), 2);
            deserializer.Read(out this.elbow_c, sizeof(double), 2);
            deserializer.Read(out this.delbow_c, sizeof(double), 2);
            deserializer.Read(out this.ddelbow_c, sizeof(double), 2);
            deserializer.Read(out this.tau_J, sizeof(double), 7);
            deserializer.Read(out this.tau_J_d, sizeof(double), 7);
            deserializer.Read(out this.dtau_J, sizeof(double), 7);
            deserializer.Read(out this.q, sizeof(double), 7);
            deserializer.Read(out this.q_d, sizeof(double), 7);
            deserializer.Read(out this.dq, sizeof(double), 7);
            deserializer.Read(out this.dq_d, sizeof(double), 7);
            deserializer.Read(out this.ddq_d, sizeof(double), 7);
            deserializer.Read(out this.joint_contact, sizeof(double), 7);
            deserializer.Read(out this.cartesian_contact, sizeof(double), 6);
            deserializer.Read(out this.joint_collision, sizeof(double), 7);
            deserializer.Read(out this.cartesian_collision, sizeof(double), 6);
            deserializer.Read(out this.tau_ext_hat_filtered, sizeof(double), 7);
            deserializer.Read(out this.O_F_ext_hat_K, sizeof(double), 6);
            deserializer.Read(out this.K_F_ext_hat_K, sizeof(double), 6);
            deserializer.Read(out this.O_dP_EE_d, sizeof(double), 6);
            deserializer.Read(out this.O_T_EE_c, sizeof(double), 16);
            deserializer.Read(out this.O_dP_EE_c, sizeof(double), 6);
            deserializer.Read(out this.O_ddP_EE_c, sizeof(double), 6);
            deserializer.Read(out this.theta, sizeof(double), 7);
            deserializer.Read(out this.dtheta, sizeof(double), 7);
            this.current_errors = ErrorsMsg.Deserialize(deserializer);
            this.last_motion_errors = ErrorsMsg.Deserialize(deserializer);
            deserializer.Read(out this.control_command_success_rate);
            deserializer.Read(out this.robot_mode);
            deserializer.Read(out this.robot_time);
            deserializer.Read(out this.gripper_width);
            deserializer.Read(out this.gripper_max_width);
            deserializer.Read(out this.gripper_is_grasped);
            deserializer.Read(out this.gripper_temperature);
            deserializer.Read(out this.gripper_time);
            deserializer.Read(out this.is_fresh);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.header);
            serializer.Write(this.pose_desired);
            serializer.Write(this.O_T_EE);
            serializer.Write(this.O_T_EE_d);
            serializer.Write(this.F_T_EE);
            serializer.Write(this.EE_T_K);
            serializer.Write(this.m_ee);
            serializer.Write(this.I_ee);
            serializer.Write(this.F_x_Cee);
            serializer.Write(this.m_load);
            serializer.Write(this.I_load);
            serializer.Write(this.F_x_Cload);
            serializer.Write(this.m_total);
            serializer.Write(this.I_total);
            serializer.Write(this.F_x_Ctotal);
            serializer.Write(this.elbow);
            serializer.Write(this.elbow_d);
            serializer.Write(this.elbow_c);
            serializer.Write(this.delbow_c);
            serializer.Write(this.ddelbow_c);
            serializer.Write(this.tau_J);
            serializer.Write(this.tau_J_d);
            serializer.Write(this.dtau_J);
            serializer.Write(this.q);
            serializer.Write(this.q_d);
            serializer.Write(this.dq);
            serializer.Write(this.dq_d);
            serializer.Write(this.ddq_d);
            serializer.Write(this.joint_contact);
            serializer.Write(this.cartesian_contact);
            serializer.Write(this.joint_collision);
            serializer.Write(this.cartesian_collision);
            serializer.Write(this.tau_ext_hat_filtered);
            serializer.Write(this.O_F_ext_hat_K);
            serializer.Write(this.K_F_ext_hat_K);
            serializer.Write(this.O_dP_EE_d);
            serializer.Write(this.O_T_EE_c);
            serializer.Write(this.O_dP_EE_c);
            serializer.Write(this.O_ddP_EE_c);
            serializer.Write(this.theta);
            serializer.Write(this.dtheta);
            serializer.Write(this.current_errors);
            serializer.Write(this.last_motion_errors);
            serializer.Write(this.control_command_success_rate);
            serializer.Write(this.robot_mode);
            serializer.Write(this.robot_time);
            serializer.Write(this.gripper_width);
            serializer.Write(this.gripper_max_width);
            serializer.Write(this.gripper_is_grasped);
            serializer.Write(this.gripper_temperature);
            serializer.Write(this.gripper_time);
            serializer.Write(this.is_fresh);
        }

        public override string ToString()
        {
            return "RobotStateMsg: " +
            "\nheader: " + header.ToString() +
            "\npose_desired: " + System.String.Join(", ", pose_desired.ToList()) +
            "\nO_T_EE: " + System.String.Join(", ", O_T_EE.ToList()) +
            "\nO_T_EE_d: " + System.String.Join(", ", O_T_EE_d.ToList()) +
            "\nF_T_EE: " + System.String.Join(", ", F_T_EE.ToList()) +
            "\nEE_T_K: " + System.String.Join(", ", EE_T_K.ToList()) +
            "\nm_ee: " + m_ee.ToString() +
            "\nI_ee: " + System.String.Join(", ", I_ee.ToList()) +
            "\nF_x_Cee: " + System.String.Join(", ", F_x_Cee.ToList()) +
            "\nm_load: " + m_load.ToString() +
            "\nI_load: " + System.String.Join(", ", I_load.ToList()) +
            "\nF_x_Cload: " + System.String.Join(", ", F_x_Cload.ToList()) +
            "\nm_total: " + m_total.ToString() +
            "\nI_total: " + System.String.Join(", ", I_total.ToList()) +
            "\nF_x_Ctotal: " + System.String.Join(", ", F_x_Ctotal.ToList()) +
            "\nelbow: " + System.String.Join(", ", elbow.ToList()) +
            "\nelbow_d: " + System.String.Join(", ", elbow_d.ToList()) +
            "\nelbow_c: " + System.String.Join(", ", elbow_c.ToList()) +
            "\ndelbow_c: " + System.String.Join(", ", delbow_c.ToList()) +
            "\nddelbow_c: " + System.String.Join(", ", ddelbow_c.ToList()) +
            "\ntau_J: " + System.String.Join(", ", tau_J.ToList()) +
            "\ntau_J_d: " + System.String.Join(", ", tau_J_d.ToList()) +
            "\ndtau_J: " + System.String.Join(", ", dtau_J.ToList()) +
            "\nq: " + System.String.Join(", ", q.ToList()) +
            "\nq_d: " + System.String.Join(", ", q_d.ToList()) +
            "\ndq: " + System.String.Join(", ", dq.ToList()) +
            "\ndq_d: " + System.String.Join(", ", dq_d.ToList()) +
            "\nddq_d: " + System.String.Join(", ", ddq_d.ToList()) +
            "\njoint_contact: " + System.String.Join(", ", joint_contact.ToList()) +
            "\ncartesian_contact: " + System.String.Join(", ", cartesian_contact.ToList()) +
            "\njoint_collision: " + System.String.Join(", ", joint_collision.ToList()) +
            "\ncartesian_collision: " + System.String.Join(", ", cartesian_collision.ToList()) +
            "\ntau_ext_hat_filtered: " + System.String.Join(", ", tau_ext_hat_filtered.ToList()) +
            "\nO_F_ext_hat_K: " + System.String.Join(", ", O_F_ext_hat_K.ToList()) +
            "\nK_F_ext_hat_K: " + System.String.Join(", ", K_F_ext_hat_K.ToList()) +
            "\nO_dP_EE_d: " + System.String.Join(", ", O_dP_EE_d.ToList()) +
            "\nO_T_EE_c: " + System.String.Join(", ", O_T_EE_c.ToList()) +
            "\nO_dP_EE_c: " + System.String.Join(", ", O_dP_EE_c.ToList()) +
            "\nO_ddP_EE_c: " + System.String.Join(", ", O_ddP_EE_c.ToList()) +
            "\ntheta: " + System.String.Join(", ", theta.ToList()) +
            "\ndtheta: " + System.String.Join(", ", dtheta.ToList()) +
            "\ncurrent_errors: " + current_errors.ToString() +
            "\nlast_motion_errors: " + last_motion_errors.ToString() +
            "\ncontrol_command_success_rate: " + control_command_success_rate.ToString() +
            "\nrobot_mode: " + robot_mode.ToString() +
            "\nrobot_time: " + robot_time.ToString() +
            "\ngripper_width: " + gripper_width.ToString() +
            "\ngripper_max_width: " + gripper_max_width.ToString() +
            "\ngripper_is_grasped: " + gripper_is_grasped.ToString() +
            "\ngripper_temperature: " + gripper_temperature.ToString() +
            "\ngripper_time: " + gripper_time.ToString() +
            "\nis_fresh: " + is_fresh.ToString();
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize);
        }
    }
}
